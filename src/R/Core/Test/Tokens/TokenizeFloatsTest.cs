﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.Languages.Core.Tests.Tokens;
using Microsoft.R.Core.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.R.Core.Test.Tokens {
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class TokenizeFloatsTest : TokenizeTestBase<RToken, RTokenType> {
        [TestMethod]
        [TestCategory("R.Tokenizer")]
        public void TokenizeFloats1() {
            var tokens = this.Tokenize("+1 ", new RTokenizer());

            Assert.AreEqual(1, tokens.Count);
            Assert.AreEqual(RTokenType.Number, tokens[0].TokenType);
            Assert.AreEqual(0, tokens[0].Start);
            Assert.AreEqual(2, tokens[0].Length);
        }

        [TestMethod]
        [TestCategory("R.Tokenizer")]
        public void TokenizeFloats2() {
            var tokens = this.Tokenize("-.0", new RTokenizer());

            Assert.AreEqual(1, tokens.Count);
            Assert.AreEqual(RTokenType.Number, tokens[0].TokenType);
            Assert.AreEqual(0, tokens[0].Start);
            Assert.AreEqual(3, tokens[0].Length);
        }

        [TestMethod]
        [TestCategory("R.Tokenizer")]
        public void TokenizeFloats3() {
            var tokens = this.Tokenize("0.e1", new RTokenizer());

            Assert.AreEqual(1, tokens.Count);
            Assert.AreEqual(RTokenType.Number, tokens[0].TokenType);
            Assert.AreEqual(0, tokens[0].Start);
            Assert.AreEqual(4, tokens[0].Length);
        }

        [TestMethod]
        [TestCategory("R.Tokenizer")]
        public void TokenizeFloats4() {
            var tokens = this.Tokenize(".0e-2", new RTokenizer());

            Assert.AreEqual(1, tokens.Count);
            Assert.AreEqual(RTokenType.Number, tokens[0].TokenType);
            Assert.AreEqual(0, tokens[0].Start);
            Assert.AreEqual(5, tokens[0].Length);
        }

        [TestMethod]
        [TestCategory("R.Tokenizer")]
        public void TokenizeFloats5() {
            var tokens = this.Tokenize("-0.e", new RTokenizer());
            Assert.AreEqual(0, tokens.Count);
        }

        [TestMethod]
        [TestCategory("R.Tokenizer")]
        public void TokenizeFloats6() {
            var tokens = this.Tokenize("-12.%foo%-.1e", new RTokenizer());

            Assert.AreEqual(2, tokens.Count);
            Assert.AreEqual(RTokenType.Number, tokens[0].TokenType);
            Assert.AreEqual(0, tokens[0].Start);
            Assert.AreEqual(4, tokens[0].Length);

            Assert.AreEqual(RTokenType.Operator, tokens[1].TokenType);
            Assert.AreEqual(4, tokens[1].Start);
            Assert.AreEqual(5, tokens[1].Length);
        }

        [TestMethod]
        [TestCategory("R.Tokenizer")]
        public void TokenizeFloats7() {
            var tokens = this.Tokenize(".1", new RTokenizer());

            Assert.AreEqual(1, tokens.Count);
            Assert.AreEqual(RTokenType.Number, tokens[0].TokenType);
            Assert.AreEqual(0, tokens[0].Start);
            Assert.AreEqual(2, tokens[0].Length);
        }

        [TestMethod]
        [TestCategory("R.Tokenizer")]
        public void TokenizeFloats8() {
            var tokens = this.Tokenize("1..1", new RTokenizer());

            Assert.AreEqual(2, tokens.Count);
            Assert.AreEqual(RTokenType.Number, tokens[0].TokenType);
            Assert.AreEqual(0, tokens[0].Start);
            Assert.AreEqual(2, tokens[0].Length);

            Assert.AreEqual(RTokenType.Number, tokens[1].TokenType);
            Assert.AreEqual(2, tokens[1].Start);
            Assert.AreEqual(2, tokens[1].Length);
        }

        [TestMethod]
        [TestCategory("R.Tokenizer")]
        public void TokenizeFloats9() {
            var tokens = this.Tokenize("1e", new RTokenizer());
            Assert.AreEqual(0, tokens.Count);
        }

        [TestMethod]
        [TestCategory("R.Tokenizer")]
        public void TokenizeFile_FloatsFile() {
            TokenizeFiles.TokenizeFile(this.TestContext, @"Tokenization\Floats.r");
        }
    }
}
