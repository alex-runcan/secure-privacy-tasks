namespace Domain.Exceptions;

public class BinaryStringPrefixEvaluationFailedException() : Exception("Binary string prefix evaluation failed, there are more 0's than 1's")
{
}