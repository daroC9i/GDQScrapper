using System;

namespace GDQScrapper.Core.Domain.EventData
{
    public class FavoriteState
    {
        public bool IsFavorite { get; } 

        public FavoriteState(){ }

        public FavoriteState(bool isFavorite)
        {
            IsFavorite = isFavorite;
        }

        public override string ToString()
        {
            return IsFavorite ? "true" : "false";
        }
    }
}
