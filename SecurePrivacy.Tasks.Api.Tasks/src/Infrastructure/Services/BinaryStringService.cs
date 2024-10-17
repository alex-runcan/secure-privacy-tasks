using Domain.Exceptions;
using Domain.Interfaces.Services;

namespace Infrastructure.Services;

public class BinaryStringService : IBinaryStringService
{
    public string ValidateBinaryString(string value)
    {
        var zeroBytesCount = 0;

        if (value.Length % 2 != 0) //if the input length is odd then it's impossible to have equal number of 0's and 1's!
        {
            throw new BinaryStringUnbalancedBytesException();
        }

        if (value.Length <= 2)
        {
            return ValidateMinimumLengthBinaryString(value);
        }

        
        for (var index = 0; index < value.Length; index++)
        {
            var character = value[index];
            if (!IsCharacterBinary(character))
            {
                throw new ArgumentException("Binary strings must only contain 1's and 0's");
            }

            if (character is '0')
            {
                zeroBytesCount++;
            }

            if (DoesPrefixContainsMoreZeroBits(index, zeroBytesCount, value))
            {
                throw new BinaryStringPrefixEvaluationFailedException();
            }

            if (IsStringAlreadyUnbalanced(index, zeroBytesCount, value))
            {
                throw new BinaryStringUnbalancedBytesException();
            }
        }

        if (!DoesStringContainsEqualNumberOfZeroAndOneBits(zeroBytesCount, value))
        {
            throw new BinaryStringUnbalancedBytesException();
        }

        return "The binary string satisfies all the conditions!";
    }

    private string ValidateMinimumLengthBinaryString(string value)
    {
        if (value.Length == 0)
        {
            throw new BinaryStringEmptyException();
        }
        
        if (value.Length < 2)
        {
            throw new BinaryStringUnbalancedBytesException();
        }

        if (value[0] == '0')
        {
            throw new BinaryStringPrefixEvaluationFailedException();
        }

        return "The binary string satisfies all the conditions!";
    }

    private bool IsCharacterBinary(char character)
    {
        return character is '1' || character is '0';
    }

    private bool DoesPrefixContainsMoreZeroBits(int index, int zeroBytesCount, string value)
    {
        var middleIndex = value.Length / 2 - 1;
        return index == middleIndex && zeroBytesCount > value.Length / 2 - zeroBytesCount;
    }

    private bool IsStringAlreadyUnbalanced(int index, int zeroBytesCount, string value)
    {
        var middleIndex = value.Length / 2 - 1;
        return index > middleIndex && Math.Max(zeroBytesCount, value.Length / 2 - zeroBytesCount) > value.Length / 2;
    }

    private bool DoesStringContainsEqualNumberOfZeroAndOneBits(int zeroBytesCount, string value)
    {
        return zeroBytesCount == value.Length / 2;
    }
}