using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SOC.IoT.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.IoT.OptionsSetup;
public sealed class DeviceOptionSetup : IConfigureOptions<DeviceOptions> {

    private readonly IConfiguration _configuration;

    public DeviceOptionSetup(IConfiguration configuration) => _configuration = configuration;

    public void Configure(DeviceOptions options) =>
        _configuration.GetSection(DeviceOptions.SectionName).Bind(options);
}
