namespace RecuperacionT2.Models.Entidades
{
    public class Usuario
    {
       
            private string nombreusuario;
            private string clave;
            private int idusuario;
           
            public Usuario()
            {

            }

            public Usuario(string nombreusuario, string clave)
            {
                this.nombreusuario = nombreusuario;
                this.clave = clave;
                
            }

        public string Nombreusuario { get => nombreusuario; set => nombreusuario = value; }
            public string Clave { get => clave; set => clave = value; }
        public int Idusuario { get => idusuario; set => idusuario = value; }
    }
}
