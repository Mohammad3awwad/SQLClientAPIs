using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsDA.Model
{
    public  class ClientsModel
    {
        public int ClientId { get;private set; }

        [Required]
        [NotNull]
        [MinLength(3, ErrorMessage = "Please Enter a Valid client Name")]
        [MaxLength(55, ErrorMessage = "Please Enter a Valid client Name")]
        public string ClientName { get; private set; }


        [Required]
        [NotNull]
        [MinLength(2, ErrorMessage = "Please Enter a Valid Project Name")]
        [MaxLength(100, ErrorMessage = "Please Enter a Valid Project Name")]
        public string ProjectName { get; private set; }


        public ClientsModel(int ClientId, string ClientName,string ProjectName)
        {
            this.ClientName = ClientName;
            this.ClientId = ClientId;
            this.ProjectName = ProjectName;
        }
    }
}
