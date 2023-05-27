using System;

namespace Sep6BackEnd.DataAccess.TMDBAccess;

public class TmdbException : Exception
{
    public TmdbException(string message): base (message)
    {
    }
}