﻿using Xunit;
using FluentAssertions;

namespace Cake.Newman.Tests
{
    public sealed class NewmanSettingsTests
    {
        public sealed class TheFlagTests
        {
            [Fact]
            public void ShouldIncludeInsecureWhenSet()
            {
                // Given
                var fixture = new NewmanFixture();
                fixture.Settings.DisableStrictSSL = true;

                // When
                var result = fixture.Run();

                // Then
                result.Args().Should().Be("--insecure");
            }

            [Fact]
            public void ShouldIncludeIgnoreRedirectsWhenSet() {
                var fixture = new NewmanFixture();
                fixture.Settings.IgnoreRedirects = true;

                // When
                var result = fixture.Run();

                // Then
                result.Args().Should().Be("--ignore-redirects");
            }

            [Fact]
            public void ShouldIncludeBailWhenSet() {
                var fixture = new NewmanFixture();
                fixture.Settings.ExitOnFirstFailure = true;

                // When
                var result = fixture.Run();

                // Then
                result.Args().Should().Be("--bail");
            }

        }

        [Theory]
        [InlineData(1000)]
        [InlineData(5000)]
        public void ShouldSetRequestDelayWhenSet(int delay) {
            // Given
            var fixture = new NewmanFixture();
            fixture.Settings.RequestDelay = delay;

            // When
            var result = fixture.Run();

            // Then
            result.Args().Should().Be($"--delay-request {delay}");
        }

        [Theory]
        [InlineData(2000)]
        [InlineData(3000)]
        public void ShouldSpecifyRequestTimeoutWhenSet(int timeout) {
            // Given
            var fixture = new NewmanFixture();
            fixture.Settings.RequestTimeout = timeout;

            // When
            var result = fixture.Run();

            // Then
            result.Args().Should().Be($"--timeout-request {timeout}");
        }

        [Fact]
        public void ShouldSpecifyFolderWhenSet() {
            // Given
            var fixture = new NewmanFixture();
            fixture.Settings.Folder = "Api Service";

            // When
            var result = fixture.Run();

            // Then
            result.Args().Should().Be("--folder \"Api Service\"", "Cake should have quoted this argument");
        }

        public sealed class TheExportPaths {
            [Fact]
            public void ShouldSpecifyCollectionExportPath() {
                // Given
                var path = "./export/collection.json";
                var fixture = new NewmanFixture();
                fixture.Settings.ExportCollectionPath = path;

                // When
                var result = fixture.Run();

                // Then
                result.Args().Should().Be("--export-collection \"export/collection.json\"", "file paths are quoted and trimmed"); 
            }

            [Fact]
            public void ShouldSpecifyEnvironmentExportPath() {
                // Given
                var fixture = new NewmanFixture();
                fixture.Settings.ExportEnvironmentPath = "./export/environment.json";

                // When
                var result = fixture.Run();

                // Then
                result.Args().Should().Be("--export-environment \"export/environment.json\"", "file paths are quoted and trimmed");
            }

            [Fact]
            public void ShouldSpecifyGlobalsPath() {
                // Given
                var fixture = new NewmanFixture();
                fixture.Settings.ExportGlobalsPath = "./export/globals.json";

                // When
                var result = fixture.Run();

                // Then
                result.Args().Should().Be("--export-globals \"export/globals.json\"", "file paths are quoted and trimmed");
            }
        }

        public sealed class TheFilePaths {
            [Fact]
            public void ShouldSpecifyEnvironmentsFile() {
                // Given
                var fixture = new NewmanFixture();
                fixture.Settings.EnvironmentFile = "./collection.json";

                // When
                var result = fixture.Run();

                // Then
                result.Args().Should().Be("--environment \"collection.json\"", "file paths are quoted and trimmed");
            }

            [Fact]
            public void ShouldSpecifyGlobalsFile() {
                // Given
                var fixture = new NewmanFixture();
                fixture.Settings.GlobalVariablesFile = "./vars/globals.json";

                // When
                var result = fixture.Run();

                // Then
                result.Args().Should().Be("--globals \"vars/globals.json\"", "file paths are quoted and trimmed");
            }
        }
    }
}
