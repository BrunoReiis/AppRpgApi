using AppRpgEtec.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AppRpgEtec.Services.Usuarios
{
    internal class UsuarioService
    {
        private string _token;
        private readonly Request _request;
        private const string apiUrlBase = "https://bsite.net/luizfernando987/Usuario";

        public UsuarioService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public UsuarioService()
        {
            _request = new Request();
        }

        public async Task<Usuario> PutAtualizarLocalizacaoAsync(Usuario u)
        {
            string urlComplementar = "/AtualizarLocalizacao";
            var result = await _request.PutAsync(apiUrlBase + urlComplementar, u, _token);
            return result;
        }
        public async Task<Usuario> GetUsuarioAsync(string login)
        {
            string urlComplementar = string.Format("/GetByLogin/{0}", login);
            var usuario = await
                _request.GetAsync<Models.Usuario>(apiUrlBase + urlComplementar, _token);
            return usuario;
        }
        public async Task<ObservableCollection<Usuario>> GetUsuariosAsync()
        {
            string urlComplementar = string.Format("{0}", "/GetAll");
            ObservableCollection<Models.Usuario> listaUsuarios = await
                _request.GetAsync<ObservableCollection<Models.Usuario>>(apiUrlBase + urlComplementar, _token);
            return listaUsuarios;
        }
    }
}
