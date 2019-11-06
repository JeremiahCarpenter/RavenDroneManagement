using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace RavenBLL
{
    public class MeaningfulCaluclation
    {
        
        public Decimal Calculate_FineAmount(int recordSpeed,int legalSpeed,int historyCount)
        {
            Decimal fineAmount = 0;
            int dif = recordSpeed - legalSpeed;
            if (10<=dif)
            {
                if (dif >= 15) 
                {
                    fineAmount = 500;
                }
                else if ((dif >= 11) && (dif < 15))
                {
                    fineAmount = 90;
                }

                if ((historyCount > 0) && (historyCount <= 3))
                {
                    switch(historyCount)
                    {
                        case 1:
                            fineAmount = fineAmount * 2;
                            break; 
                        case 2:
                            fineAmount = fineAmount * 3;
                            break;
                        case 3:
                            fineAmount = (fineAmount * 3) + 500;
                            break;
                        

                    }
                }
            }

            return fineAmount;
        }
    }
}
