using System.Linq;
using proyecto_ing_software_api.Data;

namespace proyecto_ing_software_api.Security
{
    public class ValidacionUsuario
    {
        private ApplicationDbContext context;
        public ValidacionUsuario(ApplicationDbContext _context){
            context = _context;
        }
        public  bool Login(string user, string password){
            var log = context.FC_USUARIOS.Where(wr => wr.USUARIO.Equals(user) && wr.PASSWORD.Equals(password)).FirstOrDefault();
            if(log != null){
                return true;
            }else{
                return false;
            }
        }
    }
}