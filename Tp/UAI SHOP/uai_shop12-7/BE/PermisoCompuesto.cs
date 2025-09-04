using System.Collections.Generic;

namespace BE
{
    public class PermisoCompuesto : Perfil
    {
        private List<Perfil> _hijos = new List<Perfil>();

        public void Agregar(Perfil permiso)
        {
            _hijos.Add(permiso);
        }

        public void Quitar(Perfil permiso)
        {
            _hijos.Remove(permiso);
        }

        public override List<Perfil> GetPermisos()
        {
            List<Perfil> todos = new List<Perfil>();

            foreach (var hijo in _hijos)
            {
                todos.AddRange(hijo.GetPermisos());
            }

            return todos;
        }
    }
}
