namespace Domain.Exceptions;

public class BinaryStringUnbalancedBytesException() : Exception("Binary string doesn't have an equal number of 0's and 1's!")
{
}