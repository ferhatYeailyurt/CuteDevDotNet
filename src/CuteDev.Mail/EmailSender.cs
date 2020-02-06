/* Author: Volkan ŞENDAĞ - vsendag@gmail.com */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using CuteDev.Mail.Parameters;

namespace CuteDev.Mail
{
    /// <summary>
    /// E-posta gönderim işlemlerini yapar (volkansendag - 2014.12.04)
    /// </summary>
    public class EmailSender
    {
        #region Globals

        SmtpClient sender = new SmtpClient();

        #endregion

        #region Properties

        #region Sender

        /// <summary>
        /// Sunucu (volkansendag - 2014.12.04)
        /// </summary>
        public string Host
        {
            get { return sender.Host; }
            set { sender.Host = value; }
        }

        /// <summary>
        /// Sunucu portu (volkansendag - 2014.12.04)
        /// </summary>
        public int Port
        {
            get { return sender.Port; }
            set { sender.Port = value; }
        }

        /// <summary>
        /// Gönderen e-posta adresi (volkansendag - 2014.12.04)
        /// </summary>
        public string SenderEmailAddress { get; set; }

        /// <summary>
        /// Gönderen e-posta adresi şifresi (volkansendag - 2014.12.04)
        /// </summary>
        public string SenderEmailPassword { get; set; }

        /// <summary>
        /// SSL desteği (volkansendag - 2014.12.04)
        /// </summary>
        public bool EnableSsl
        {
            get { return sender.EnableSsl; }
            set { sender.EnableSsl = value; }
        }

        public string SenderIdentity { get; set; }

        public string SenderEmailUser { get; set; }

        #endregion

        #region Message

        /// <summary>
        /// E-postanın öncelik durumu (volkansendag - 2014.12.04)
        /// </summary>
        public MailPriority Priority { get; set; }

        /// <summary>
        /// E-postanın içeriğinin HTML olup olmadığı (volkansendag - 2014.12.04)
        /// </summary>
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// E-posta kararkter kodlaması (volkansendag - 2014.12.04)
        /// </summary>
        public System.Text.Encoding Encoding { get; set; }

        #endregion

        /// <summary>
        /// E-posta gönderim hatası (volkansendag - 2014.12.04)
        /// </summary>
        public Exception Error { get; protected set; }

        #endregion

        #region Constructor

        public EmailSender()
        {
            this.Host = ConfigurationManager.AppSettings["SmtpSunucu"];
            this.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
            this.SenderEmailAddress = ConfigurationManager.AppSettings["GonderenPostaAdresi"];
            this.SenderEmailPassword = ConfigurationManager.AppSettings["GonderenPostaSifresi"];
            this.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["SSLKullanimi"]);
            this.SenderIdentity = ConfigurationManager.AppSettings["GonderenKimligi"];
            this.SenderEmailUser = ConfigurationManager.AppSettings["GonderenPostaKullanicisi"];
            Initiliaze();
        }

        public EmailSender(string SmtpSunucu, int SmtpPort, string GonderenPostaAdresi, string GonderenPostaSifresi, bool SSLKullanimi, string GonderenKimligi, string GonderenPostaKullanicisi)
        {
            this.Host = SmtpSunucu;
            this.Port = SmtpPort;
            this.SenderEmailAddress = GonderenPostaAdresi;
            this.SenderEmailPassword = GonderenPostaSifresi;
            this.EnableSsl = SSLKullanimi;
            this.SenderIdentity = GonderenKimligi;
            this.SenderEmailUser = GonderenPostaKullanicisi;
        }

        void Initiliaze()
        {
            this.Priority = MailPriority.Normal;
            this.IsBodyHtml = false;
            this.Encoding = System.Text.Encoding.UTF8;

            sender.UseDefaultCredentials = true;
            sender.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        #endregion

        #region Functions

        /// <summary>
        /// E-posta gönderir (volkansendag - 2014.12.04)
        /// </summary>
        protected void Send(MailMessage msg)
        {
            this.Error = null;
            sender.Credentials = new System.Net.NetworkCredential(this.SenderEmailAddress, this.SenderEmailPassword);
            msg.From = new System.Net.Mail.MailAddress(this.SenderEmailAddress);
            msg.Priority = this.Priority;
            msg.IsBodyHtml = this.IsBodyHtml;
            msg.SubjectEncoding = this.Encoding;
            msg.BodyEncoding = this.Encoding;

            try
            {
                sender.Send(msg);
            }
            catch (Exception ex)
            {
                this.Error = ex;
                throw ex;
            }
        }

        /// <summary>
        /// E-posta gönderir (volkansendag - 2014.12.04)
        /// </summary>
        public void Send(string to, string subject, string body, List<Attachment> attachs)
        {
            MailMessage msg = new MailMessage();
            foreach (var item in to.Split(';'))
            {
                msg.To.Add(new System.Net.Mail.MailAddress(item));
            }

            msg.Subject = subject;
            msg.Body = body;

            if (attachs != null)
            {
                foreach (var attach in attachs)
                {
                    msg.Attachments.Add(attach);
                }
            }

            this.Send(msg);
        }

        /// <summary>
        /// E-posta gönderir (volkansendag - 2014.12.04)
        /// </summary>
        public void Send(pSend prms)
        {
            List<Attachment> attachs = null;
            if (prms.Attachments != null && prms.Attachments.Count > 0)
            {

                attachs = new List<Attachment>();

                foreach (var a in prms.Attachments)
                {
                    if (a.Content == null || a.Content.Length <= 0)
                        break;

                    MemoryStream stream = new MemoryStream(a.Content);
                    attachs.Add(new Attachment(stream, a.Name));
                }
            }

            this.Send(prms.To, prms.Subject, prms.Content, attachs);
        }

        #endregion
    }
}
