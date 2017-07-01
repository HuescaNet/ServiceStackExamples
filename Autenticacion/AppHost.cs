using System.Collections.Generic;
using Funq;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;

namespace Autenticacion
{
    // Clase host de los servicios
    public class AppHost : AppSelfHostBase
    {
        // Configura el host con todos los servicios que se hayan definido en en el mismo assembly
        public AppHost() : base("Servicio", typeof(AppHost).GetAssembly()) { }

        // Configuración del host
        public override void Configure(Container container)
        {
            // Configura la característica de autenticación y la de sesiones de usuario
            Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                new IAuthProvider[] {
                    new BasicAuthProvider(),
                    new CredentialsAuthProvider(),
                }));

            Plugins.Add(new RegistrationFeature());

            container.Register<ICacheClient>(new MemoryCacheClient());

            // Clase para gestionar los usuarios de la aplicación
            var repositorioUsuarios = new InMemoryAuthRepository();
            container.Register<IUserAuthRepository>(repositorioUsuarios);

            // Crea un usuario con nombre de usuario, contraseña, roles y permisos
            // Para poder acceder a los servicios debe tener rol Administrador y permisos EnviarPeticiones
            CrearUsuario(repositorioUsuarios, 1, "user", "password", new List<string> { "NoAdministrador" }, new List<string> { "NoEnviarPeticiones" });
        }

        private void CrearUsuario(InMemoryAuthRepository repositorio, int id, string username, string password, List<string> roles = null, List<string> permissions = null)
        {
            string hash;
            string salt;
            new SaltedHash().GetHashAndSaltString(password, out hash, out salt);

            repositorio.CreateUserAuth(new UserAuth
            {
                Id = id,
                UserName = username,
                PasswordHash = hash,
                Salt = salt,
                Roles = roles,
                Permissions = permissions
            }, password);
        }
    }
}