using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc.Mailer;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModelSite.Models
{
    public class JoinViewModel
    {
        [DisplayName("Имя")]
        [Required]
        public string Name { get; set; }
       // public string SecondName { get; set; }


        [DisplayName("Рост")]
        [Required]
        [Range(0,300)]
        public double Height { get; set; }
        
        
        [DisplayName("Возраст")]
        [Required]
        [Range(14, 30)]
        public double Age { get; set; }
        
        
        [DisplayName("Телефон")]
        [Required]
        public int Phone { get; set; }

        [DisplayName("E-mail")]
        [EmailAddress]
        public string Mail { get; set; }
        public HttpPostedFileBase FullPhoto {get;set;}
        public HttpPostedFileBase HalfPhoto { get; set; }
        
        

        public String CreateMessage()
        {
            var body = new System.Text.StringBuilder();
            body.Append("Имя: " + Name + "<br/>");
            body.Append("Рост: " + Height + "<br/>");
            body.Append("Возраст: " + Age + "<br/>");
            body.Append("Телефон: " + Phone + "<br/>");
            if(Mail!=null && Mail!=String.Empty)
                body.Append("E-mail: " + Mail);

            return body.ToString();
        }

    }
}