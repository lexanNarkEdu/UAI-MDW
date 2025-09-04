using System.Collections.Generic;

namespace BE
{
    public abstract class Perfil
    {
        public string Nombre { get; set; }

        public abstract List<Perfil> GetPermisos();
    }
}
