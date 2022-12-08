using System.Text.RegularExpressions;

namespace EosTorrent;

public class MagnetLink
    {
        public string? DisplayName { get; }
        public IList<string> Trackers { get; }
        public string Hash { get; }

        /// <summary>
        /// Magnet link parser
        /// </summary>
        /// <param name="magnetLink">Magnet link</param>
        /// <exception cref="ArgumentException">Throws if magnet has no hash</exception>
        public MagnetLink(string magnetLink)
        {
            DisplayName = ParseDisplayName(magnetLink);
            Trackers = ParseTrackers(magnetLink);
            Hash = ParseHash(magnetLink);
        }

        private static string? ParseDisplayName(string magnetLink)
        {
            var displayNameMatch = Regex.Match(magnetLink, "dn=([^&]*)");
            if (displayNameMatch.Success)
            {
                return Uri.UnescapeDataString(displayNameMatch.Groups[1].Value);
            }
            return null;
        }

        private static IList<string> ParseTrackers(string magnetLink)
        {
            var trackersMatch = Regex.Matches(magnetLink, "tr=([^&]*)");
            return trackersMatch.Select(m => Uri.UnescapeDataString(m.Groups[1].Value)).ToList();
        }
        private static string ParseHash(string magnetLink)
        {
            var hashMatch = Regex.Match(magnetLink, "xt=urn:btih:([^&]*)");
            if (hashMatch.Success)
            {
                return hashMatch.Groups[1].Value;
            }

            throw new ArgumentException("Invalid magnet: Contains no hash");
        }
    }