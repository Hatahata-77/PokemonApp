using System;
using System.ComponentModel;

namespace PokemonApp.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class TableViewModel
    {
        [DisplayName("ばんごう")]
        public int No { get; set; }

        [DisplayName("サムネイル")]
        public string Thumnail { get; set; }

        [DisplayName("なまえ")]
        public string Name { get; set; }

        [DisplayName("タイプ")]
        public string Type { get; set; }

    }
}
