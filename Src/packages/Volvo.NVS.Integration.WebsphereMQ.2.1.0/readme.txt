
NVS Integration - WMQ
======================

See Developer Handbook for more information from below teamplace link:
https://teamplace.volvo.com/sites/volvoit-dotNET/SAD/NVS%20Integration%20Library.aspx

Getting started
===============

Setting up app.config/web.config
--------------------------------

<configuration>
  <configSections>
    <section name="unity" type="Microsoft.Practices.Unity.Configuration.UnityConfigurationSection, Microsoft.Practices.Unity.Configuration"/>
    <section name="volvo.nvs.integration" type="Volvo.NVS.Integration.Configuration.IntegrationSection, Volvo.NVS.Integration"/>
  </configSections>

  <unity>
    <container>
      <register type="Volvo.NVS.Integration.IChannel, Volvo.NVS.Integration" name="wmq" mapTo="Volvo.NVS.Integration.WebsphereMQ.WmqChannel, Volvo.NVS.Integration.WebsphereMQ"/>
    </container>
  </unity>

  <volvo.nvs.integration>
    <channels>
      <add name="sample" uri="wmq://MY_HOST@MY_QMGR:2418/MY.CHANNEL?queue=MY.APP.TEST"/>
    </channels>
  </volvo.nvs.integration>

</configuration>

Sending your first message
--------------------------

using Volvo.NVS.Integration;

...

    using (var sampleChannel = ChannelFactory.Create("sample"))
    {
      sampleChannel.Publish("my first message for sample channel");
    }


Good luck!