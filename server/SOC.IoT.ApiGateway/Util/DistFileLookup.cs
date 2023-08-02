namespace SOC.IoT.ApiGateway.Util
{
    public class DistFileLookup
    {
        private readonly string[] files;

        public DistFileLookup(string distPath)
        {
            files = new DirectoryInfo(distPath).EnumerateFiles().Select(fi => fi.Name).ToArray();
        }

        public string ResolveFileName(string startsWith, string endsWith)
        {
            var fileName = files.FirstOrDefault(
                fn => fn.StartsWith(startsWith) && fn.EndsWith(endsWith)
            );
            if (fileName == null)
                throw new InvalidOperationException(
                    $"Cannot find a dist file matching the given pattern: {startsWith}*{endsWith}"
                );
            return fileName;
        }
    }
}
