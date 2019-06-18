
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
      <register name="mqin"
                type="Volvo.NVS.Integration.IInputChannel, Volvo.NVS.Integration"
                mapTo="Volvo.NVS.Integration.WebsphereMQ.InputChannel, Volvo.NVS.Integration.WebsphereMQ"/>
      <register name="mqout"
                type="Volvo.NVS.Integration.IOutputChannel, Volvo.NVS.Integration"
                mapTo="Volvo.NVS.Integration.WebsphereMQ.OutputChannel, Volvo.NVS.Integration.WebsphereMQ"/>
      <register name="mqrep"
                type="Volvo.NVS.Integration.IReplyChannel, Volvo.NVS.Integration"
                mapTo="Volvo.NVS.Integration.WebsphereMQ.ReplyChannel, Volvo.NVS.Integration.WebsphereMQ"/>
    </container>
  </unity>

  <volvo.nvs.integration>
    <channels>
      <add name="sample" uri="mqout://MY_HOST@MY_QMGR:2418/MY.CHANNEL?queue=MY.APP.TEST"/>
    </channels>
  </volvo.nvs.integration>

</configuration>

Sending your first message
--------------------------

using Volvo.NVS.Integration;

...

    using (var sampleChannel = ChannelFactory.CreateOutputChannel("sample"))
    {
      sampleChannel.Publish("my first message for sample channel");
    }


Good luck!