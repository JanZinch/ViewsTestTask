using System;
using System.IO;
using System.Threading.Tasks;
using Crypto;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Assertions;

namespace Progress
{
    public class ProgressDataAdapter : IDisposable
    {
        private const string ModelsPath = "Data/Models";
        private const string ProgressModelFileName = "progress_model.edm";
        
        private DirectoryInfo _modelsDirectoryInfo;
        private ProgressDataModel _progressDataModel;
        
        private readonly IStreamCryptoService _cryptoService;

        public ProgressDataAdapter(IStreamCryptoService cryptoService = null)
        {
            _cryptoService = cryptoService;
            
            InitializeModelsDirectory();
            LoadProgressModel();
        }

        public ProgressDataModel GetProgressModel() => _progressDataModel;
        
        private void InitializeModelsDirectory()
        {
            string fullPath = Path.Combine(Application.persistentDataPath, ModelsPath);
            _modelsDirectoryInfo = new DirectoryInfo(fullPath);
        
            if (!_modelsDirectoryInfo.Exists)
            {
                _modelsDirectoryInfo.Create();
            }
            
            Debug.Log("[ProgressDataAdapter] Target directory: " + _modelsDirectoryInfo.FullName);
        }

        private void LoadProgressModel()
        {
            string fullFileName = GetFullFileName();

            if (!File.Exists(fullFileName))
            {
                _progressDataModel = new ProgressDataModel();
                File.Create(fullFileName);
            }
            else
            {
                try
                {
                    string encryptedNotation = File.ReadAllText(fullFileName);

                    if (string.IsNullOrEmpty(encryptedNotation) || string.IsNullOrWhiteSpace(encryptedNotation))
                    {
                        _progressDataModel = new ProgressDataModel();
                    }
                    else
                    {
                        string jsonNotation = _cryptoService != null
                            ? _cryptoService.Decrypt(encryptedNotation)
                            : encryptedNotation;
                            
                        _progressDataModel = JsonConvert.DeserializeObject<ProgressDataModel>(jsonNotation);
                        Assert.IsNotNull(_progressDataModel);
                    }
                }
                catch (Exception ex)
                {
                    _progressDataModel = new ProgressDataModel();
                    
                    Exception loggedException = 
                        new Exception("[ProgressDataAdapter] Deserialization of progress_model.edm is failed. Default model created");
                    Debug.LogException(loggedException);
                            
                    File.Create(fullFileName);
                }
                
            }

            _progressDataModel.OnDemandSave += OnDemandSave;
        }
        
        private void OnDemandSave()
        {
            Task.Run(SaveProgressModel);
        }
        
        private void SaveProgressModel()
        {
            string jsonNotation = JsonConvert.SerializeObject(_progressDataModel);
            string encryptedNotation = _cryptoService != null ? _cryptoService.Encrypt(jsonNotation) : jsonNotation;
                
            lock (_progressDataModel)
            {
                File.WriteAllText(GetFullFileName(), encryptedNotation);
            }
        }

        public void ClearProgress()
        {
            File.Delete(GetFullFileName());
        }

        private string GetFullFileName()
        {
            return Path.Combine(_modelsDirectoryInfo.FullName, ProgressModelFileName);
        }

        public void Dispose()
        {
            _progressDataModel.OnDemandSave -= OnDemandSave;
        }
    }
}