﻿using APITut.Models.Dto;

namespace APITut.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>{
               new VillaDto { Id= 1, Name = "Villa1"},
               new VillaDto { Id = 2, Name = "Villa2" }
            };
    }
}
