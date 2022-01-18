using System;
using System.Collections.Generic;
using System.Text;

namespace Marche
{
    public class Cat_IA
    {


        public string Request(int _gold)
        {
            if (_gold >= 1000)
                return "Avec cet or, tu devrais debloquer une nouvelle source de production... Et pourquoi pas la ";
            else if (_gold >= 500)
                return "Eh bien, ca en fait des pieces, pourquoi pas acheter une nouvelle parcelle afin de produire plus de fruits et legumes.";
            else if (_gold >= 100)
                return "Hmm a mon avis ca serai bien que tu ameliore ton materiel !";
            else if (_gold >= 10)
                return "Je pense que tu devrais acheter des graines au marche, ca devrait faire avancer les choses";
            else
                return "Je n'ai rien a te suggerer pour le moment !";


        }

    }
}
