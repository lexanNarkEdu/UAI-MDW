using BE;
using BLL.Servicios;
using DAL;
using DAL.Servicios;
using SERVICES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UsuarioBll: IVerificable
    {
        private UsuarioDAL _repo;
        private DAO mDAO = new DAO();
        private IntegridadBL mIntegridadBL = new IntegridadBL();

        public UsuarioBll()
        {
            _repo = new UsuarioDAL();
           

        }


        public Usuario Buscar(Usuario user, bool secureIntegrity)
        {
            // Llama al método DAL que ya tenés
            user = EncriptadorService.Instance.Encriptar(user);
            Usuario usuarioDesdeDAL = _repo.Buscar(user);

            if (usuarioDesdeDAL != null && !usuarioDesdeDAL.ESTA_BLOQUEADO)
            {
                // Resetear intentos fallidos al hacer login exitoso
                _repo.resetearIntentos(user.USERNAME);

                if (secureIntegrity)
                {
                    mIntegridadBL.ActualizarSpecificDVH(nameof(Usuario), usuarioDesdeDAL.Id_Usuario);
                    mIntegridadBL.ActualizarDVV();
                }

                switch (usuarioDesdeDAL.TipoUser)
                {
                    case 1: // Webmaster
                        var webmaster = new PermisoCompuesto { Nombre = "Webmaster" };
                        webmaster.Agregar(new PermisoSimple { Nombre = "Bitácora" });
                        webmaster.Agregar(new PermisoSimple { Nombre = "Agregar productos" });
                        webmaster.Agregar(new PermisoSimple { Nombre = "Ver catálogo" });
                        webmaster.Agregar(new PermisoSimple { Nombre = "Agregar al carrito" });
                        usuarioDesdeDAL.Permiso = webmaster;
                        break;

                    

                    case 2: // Usuario común/comprador
                        var comprador = new PermisoCompuesto { Nombre = "Admin" };
                        comprador.Agregar(new PermisoSimple { Nombre = "Ver catálogo" });
                        comprador.Agregar(new PermisoSimple { Nombre = "Agregar al carrito" });
                        comprador.Agregar(new PermisoSimple { Nombre = "Agregar productos" });
                        usuarioDesdeDAL.Permiso = comprador;
                        break;

                    case 3: // Usuario común/comprador
                        var admin = new PermisoCompuesto { Nombre = "Comprador" };
                        admin.Agregar(new PermisoSimple { Nombre = "Ver catálogo" });
                        admin.Agregar(new PermisoSimple { Nombre = "Agregar al carrito" });
                        admin.Agregar(new PermisoSimple { Nombre = "Agregar productos" });
                        usuarioDesdeDAL.Permiso = admin;
                        break;
                }
            }
            else
            {
                //si existe el usuario pero no pudo ingresar, quiere decir que tipeo mal la contraseña
                //TODO busco si existe el username
                usuarioDesdeDAL = null;
                Usuario userBuscado = _repo.listar(user.USERNAME);

                if (userBuscado != null)
                {
                    if ((userBuscado.CANT_INTENTOS) < 3)
                    {
                        //si existe usuario y no llego a 3 intentos, le sumo un intento erroneo
                        _repo.sumarIntentoErroneo(userBuscado.USERNAME);
                        mIntegridadBL.ActualizarDVH();
                        mIntegridadBL.ActualizarDVV();

                    }

                    //si supero los 3 intentos lo bloqueo
                    if ((userBuscado.CANT_INTENTOS + 1) > 3 && !userBuscado.ESTA_BLOQUEADO)
                    {
                        bloquearDesbloquearUsuario(userBuscado.USERNAME);
                        mIntegridadBL.ActualizarDVH();
                        mIntegridadBL.ActualizarDVV();
                    }
                }
            }

            return usuarioDesdeDAL;
        }

        /**
        * bloquea o desbloquea el usuario en cuestion en base a su estadio
        */
        public Boolean bloquearDesbloquearUsuario(string username)
        {
            bool result= _repo.desbloquearUsuario(username);
            mIntegridadBL.ActualizarDVH();
            mIntegridadBL.ActualizarDVV();
            return result;
            
        }

        /*
         * solo lista username, cant_intentos y si esta bloqueado
         * se utiliza para la logica de bloqueo y 3 intentos fallidos
         */
        public Usuario listar(string username)
        {
            return _repo.listar(username);
        }

        /**
         * guarda la password pasada por parametro
         */
        public Boolean guardarPassEncriptada(string username, string pass)
        {
            Usuario user = new Usuario();
            user.USERNAME = username;
            user.Clave = pass;
            return _repo.guardarPassword(username, EncriptadorService.Instance.Encriptar(user).Clave);

        }

        /**
         * Lista los usuarios bloqueados
         */
        public List<String> listarUsuariosBloqueados()
        {
            return _repo.listarUsuariosBloqueados();
        }


        /// <summary>
        /// Devuelve todos los Registros de la base de datos
        /// </summary>
        /// <returns></returns>
        public List<DataRow> ObtenerRegistros()
        {
            return _repo.ObtenerRegistros();
        }
    }
}
