﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProdajaMobilnihAplikacija.Model.Requests
{
    public class KategorijaUpsertRequest
    {
        public string Naziv { get; set; }
        public string Oznaka { get; set; }
        public string Opis { get; set; }
    }
}
