using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES
{
    public class EncriptadorService
    {
        public EncriptadorService() { }

        private static EncriptadorService _instance;

        //singleton de la instancia del encriptador
        public static EncriptadorService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EncriptadorService();
                }
                return _instance;
            }
        }

        //metodo que encripta mediante Hash
        public Usuario Encriptar(Usuario user)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream2 = null;
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            stream2 = sha256.ComputeHash(encoding.GetBytes(user.Clave));
            for (int i = 0; i < stream2.Length; i++)
            {
                sb2.AppendFormat("{0:x2}", stream2[i]);
            }
            Usuario userEncriptado = new Usuario();
            userEncriptado.USERNAME = user.USERNAME;
            userEncriptado.Clave = sb2.ToString();
            return userEncriptado; // TODO poner de nuevo userEncriptado, use user solo para testing
        }

        //metodo que encripta mediante palabra secreta y forma Hash
        public Usuario Encriptar(Usuario user, string cadena)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            if (cadena.Equals("PassWord"))
            {
                stream = sha256.ComputeHash(encoding.GetBytes(user.Clave));
                for (int i = 0; i < stream.Length; i++)
                {
                    sb.AppendFormat("{0:x2}", stream[i]);
                }
                user.Clave = sb.ToString();
            }
            return user;
        }

        public string GenerarHASH(string cadena)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(cadena));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
