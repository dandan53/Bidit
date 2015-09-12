using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using Bidit.Models;

namespace Bidit.Controllers
{
    public class BidDueDateManager
    {
        private Timer Timer;

        public BidDueDateManager()
        {
            Timer = null;

            //Init();
        }

        public void Init()
        {
            InitTimer();
        }

        private void InitTimer()
        {
            try
            {
                //int Interval = Convert.ToInt32(ConfigurationManager.AppSettings["MessageTimerIntervalInSec"]);

                const int interval = 20;

                Timer = new Timer();

                Timer.AutoReset = false;
                Timer.Interval = interval * 1000;
                Timer.Elapsed += new ElapsedEventHandler(OnTimerElapsed);

                Timer.Start();
            }
            catch (Exception ex)
            {
            }
        }

        void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var itemsToRmove = new List<Item>();
                var items = DAL.Instance.GetItems();

                foreach (var itemPair in items)
                {
                    var item = itemPair.Value;
                    int result = DateTime.Compare(DateTime.Now, item.DueDate);
                    bool isBidEnded = result > 0;
                    if (isBidEnded)
                    {
                        var bidUser = DAL.Instance.GetUserByCID(item.BidCID);
                        
                        string mailSubject = "המכרז הסתיים";
                        string mailBody = "שלום " + bidUser.Username;
                        mailBody += ", \r\n";
                        mailBody += "מכרז מספר " + item.Id + " הסתיים";
                        mailBody += ". \r\n";
                        mailBody += "המחיר הטוב ביותר: " + item.FirstPrice;

                        item.IsBidEnded = true;

                        if (bidUser.IsEmailUpdates)
                        {
                            // Sending an email
                            var isMailSent = EmailSender.SendMail(bidUser.Email, mailSubject, mailBody);
                            if (isMailSent)
                            {
                                //items.Remove(item);
                            }
                        }
                    }     
                }
            }
            catch (Exception ex)
            {
            }

            Timer.Start();
        }
    }
}