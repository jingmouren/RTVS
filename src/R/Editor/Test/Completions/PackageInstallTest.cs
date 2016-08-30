﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.R.Host.Client;
using Microsoft.R.Host.Client.Host;
using Microsoft.R.Host.Client.Test.Script;
using Microsoft.UnitTests.Core.XUnit;
using Microsoft.VisualStudio.Language.Intellisense;
using Xunit;

namespace Microsoft.R.Editor.Test.Completions {
    [ExcludeFromCodeCoverage]
    [Category.R.Completion]
    [Collection(CollectionNames.NonParallel)]
    public class PackageInstallTest : RCompletionSourceTestBase, IDisposable {
        private readonly RHostBrokerConnector _brokerConnector;

        public PackageInstallTest(REditorMefCatalogFixture catalog) : base(catalog) {
            _brokerConnector = new RHostBrokerConnector();
            _brokerConnector.SwitchToLocalBroker(this.GetType().Name);
        }

        public void Dispose() {
            _brokerConnector.Dispose();
        }

        [Test]
        public async Task InstallPackageTest() {
            using (var script = new RHostScript(_exportProvider, _brokerConnector)) {
                try {
                    await script.Session.ExecuteAsync("remove.packages('abc')", REvaluationKind.Mutating);
                } catch (RException) { }

                await _packageIndex.BuildIndexAsync();

                var completionSets = new List<CompletionSet>();
                GetCompletions("abc::", 5, completionSets);

                completionSets.Should().ContainSingle();
                completionSets[0].Completions.Should().BeEmpty();

                try {
                    await script.Session.ExecuteAsync("install.packages('abc')", REvaluationKind.Mutating);
                } catch (RException) { }

                await _packageIndex.BuildIndexAsync();

                completionSets.Clear();
                GetCompletions("abc::", 5, completionSets);

                completionSets.Should().ContainSingle();
                completionSets[0].Completions.Should().NotBeEmpty();
            }
        }
    }
}
