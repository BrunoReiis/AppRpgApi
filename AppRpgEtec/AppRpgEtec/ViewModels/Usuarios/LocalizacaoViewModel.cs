using AppRpgEtec.Models;
using AppRpgEtec.Services.Usuarios;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace AppRpgEtec.ViewModels.Usuarios
{
    internal class LocalizacaoViewModel
    {

        public static Map MeuMapa;
        private UsuarioService uService;
        public LocalizacaoViewModel()
        {
            MeuMapa = new Map();

            string token = "";
            uService = new UsuarioService(token);
        }

        public async void ExibirUsuariosNoMapa()
        {
            try
            {
                ObservableCollection<Usuario> ocUsuarios = await uService.GetUsuariosAsync();

                List<Usuario> listaUsuarios = new List<Usuario>(ocUsuarios);

                foreach(Usuario u in listaUsuarios)
                {
                    if(u.Latitude != null && u.Longitude != null)
                    {
                        double latitude = (double)u.Latitude;
                        double longitude = (double)u.Longitude;

                        Pin pinAtual = new Pin()
                        {
                            Type = PinType.Place,
                            Label = u.Username,
                            Position = new Position(latitude, longitude),
                        };
                        MeuMapa.Pins.Add(pinAtual);
                    }
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        public async void InicializarMapa()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();

                if (status != PermissionStatus.Granted)
                {

                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Location))
                    {
                        await Application.Current.MainPage.DisplayAlert("Atenção", "É preciso permissão de localização", "Ok");
                    }
                    status = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
                }
                if (status == PermissionStatus.Granted)
                {
                    MeuMapa.MyLocationEnabled = true;
                    MeuMapa.UiSettings.MyLocationButtonEnabled = true;
                }
                else if (status != PermissionStatus.Unknown)
                    throw new Exception("Permissão negada");
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage
                    .DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        public async void LocalizarEscola()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();

                if (status != PermissionStatus.Granted)
                {

                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Location))
                    {
                        await Application.Current.MainPage.DisplayAlert("Atenção", "É preciso permissão de localização", "Ok");
                    }
                    status = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
                }
                if (status == PermissionStatus.Granted)
                {
                    Pin pinEtec = new Pin()
                    {
                        Type = PinType.Place,
                        Label = "Etec Horácio",
                        Address = "Rua alcântara, 113, Vila Guilherme",
                        Position = new Position(-23.5200241d, -46.596498d),
                        Tag = "IdEtec",
                    };
                    MeuMapa.Pins.Add(pinEtec);
                    MeuMapa.MoveToRegion(MapSpan.FromCenterAndRadius(pinEtec.Position, Distance.FromMeters(500)));
                }
                else if (status != PermissionStatus.Unknown)
                {
                    throw new Exception("Permissão negada");
                }
                    
            } catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", e.Message, "Ok");
            }

        }
    }
}
