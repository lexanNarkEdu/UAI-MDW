using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Usuario
    {
        public Usuario() { }

        private int id_usuario;
        public int Id_Usuario
        {
            get { return id_usuario; }
            set { id_usuario = value; }
        }
        private String username;
        public string USERNAME
        {
            get { return username; }
            set { username = value; }
        }

        private string clave;

        public string Clave
        {
            get { return clave; }
            set { clave = value; }
        }

        private int tipoUser;

        public int TipoUser
        {
            get { return tipoUser; }
            set { tipoUser = value; }
        }

        private string DVH;

        public string dvh
        {
            get { return DVH; }
            set { DVH = value; }
        }

        public Perfil Permiso { get; set; }

        private int cantIntentos;

        public int CANT_INTENTOS
        {
            get { return cantIntentos; }
            set { cantIntentos = value; }
        }

        private Boolean estaBloqueado;

        public Boolean ESTA_BLOQUEADO
        {
            get { return estaBloqueado; }
            set { estaBloqueado = value; }
        }


    }
}
