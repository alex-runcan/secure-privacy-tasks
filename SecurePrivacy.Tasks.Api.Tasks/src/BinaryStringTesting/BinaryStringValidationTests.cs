using Domain.Exceptions;
using Domain.Interfaces.Services;
using Infrastructure.Services;

namespace BinaryStringTesting;

public class BinaryStringValidationTests
{
    private IBinaryStringService _binaryStringService;

    [SetUp]
    public void Setup()
    {
        _binaryStringService = new BinaryStringService();
    }

    [Test]
        public void ValidateBinaryString_EmptyString_ThrowsBinaryStringEmptyException()
        {
            string input = "";
            Assert.Throws<BinaryStringEmptyException>(() => _binaryStringService.ValidateBinaryString(input));
        }

        [Test]
        public void ValidateBinaryString_MinimalValidLengthPrefixUnbalanced_ThrowsBinaryStringPrefixEvaluationFailedException()
        {
            string input = "01";
            Assert.Throws<BinaryStringPrefixEvaluationFailedException>(() => _binaryStringService.ValidateBinaryString(input));
        }
        
        [Test]
        public void ValidateBinaryString_MinimalValidLength_ReturnsSccessMessage()
        {
            string input = "10";
            string result = _binaryStringService.ValidateBinaryString(input);
            Assert.That(result, Is.EqualTo("The binary string satisfies all the conditions!"));
        }

        [Test]
        public void ValidateBinaryString_ValidEvenLength_ReturnsSuccessMessage()
        {
            string input = "1100";
            string result = _binaryStringService.ValidateBinaryString(input);
            Assert.That(result, Is.EqualTo("The binary string satisfies all the conditions!"));
        }

        [Test]
        public void ValidateBinaryString_ValidLongString_ReturnsSuccessMessage()
        {
            string input = "10101010";
            string result = _binaryStringService.ValidateBinaryString(input);
            Assert.That(result, Is.EqualTo("The binary string satisfies all the conditions!"));
        }

        [Test]
        public void ValidateBinaryString_OddLength_ThrowsBinaryStringUnbalancedBytesException()
        {
            string input = "010";
            Assert.Throws<BinaryStringUnbalancedBytesException>(() => _binaryStringService.ValidateBinaryString(input));
        }

        [Test]
        public void ValidateBinaryString_NonBinaryCharacter_ThrowsArgumentException()
        {
            string input = "0102";
            var ex = Assert.Throws<ArgumentException>(() => _binaryStringService.ValidateBinaryString(input));
            Assert.That(ex.Message, Is.EqualTo("Binary strings must only contain 1's and 0's"));
        }

        [Test]
        public void ValidateBinaryString_PrefixHasMoreZeros_ThrowsBinaryStringPrefixEvaluationFailedException()
        {
            string input = "0011";
            Assert.Throws<BinaryStringPrefixEvaluationFailedException>(() => _binaryStringService.ValidateBinaryString(input));
        }

        [Test]
        public void ValidateBinaryString_UnequalNumberOfZerosAndOnes_ThrowsBinaryStringUnbalancedBytesException()
        {
            string input = "1110";
            Assert.Throws<BinaryStringUnbalancedBytesException>(() => _binaryStringService.ValidateBinaryString(input));
        }

        [Test]
        public void ValidateBinaryString_AllZerosEvenLength_ThrowsBinaryStringUnbalancedBytesException()
        {
            string input = "0000";
            Assert.Throws<BinaryStringPrefixEvaluationFailedException>(() => _binaryStringService.ValidateBinaryString(input));
        }

        [Test]
        public void ValidateBinaryString_AllOnesEvenLength_ThrowsBinaryStringUnbalancedBytesException()
        {
            string input = "1111";
            Assert.Throws<BinaryStringUnbalancedBytesException>(() => _binaryStringService.ValidateBinaryString(input));
        }

        [Test]
        public void ValidateBinaryString_PrefixEvaluationFailsAtMiddle_ThrowsBinaryStringPrefixEvaluationFailedException()
        {
            string input = "010011";
            Assert.Throws<BinaryStringPrefixEvaluationFailedException>(() => _binaryStringService.ValidateBinaryString(input));
        }

        [Test]
        public void ValidateBinaryString_LongerValidString_ReturnsSuccessMessage()
        {
            string input = "11001100";
            string result = _binaryStringService.ValidateBinaryString(input);
            Assert.That(result, Is.EqualTo("The binary string satisfies all the conditions!"));
        }
}