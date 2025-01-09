using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

public class CurrentDataPublisher
{
    private readonly IMqttClient _mqttClient;

    public CurrentDataPublisher()
    {
        var factory = new MqttFactory();
        _mqttClient = factory.CreateMqttClient();
    }

    public async Task InitializeAndPublishAsync()
    {
        try
        {
            // MQTT 브로커에 연결
            await ConnectToBrokerAsync("43.203.159.137", 1883);

            // CurrentData를 MQTT로 전송
            await PublishCurrentDataAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MQTT 작업 중 오류 발생: {ex.Message}");
        }
    }

    private async Task ConnectToBrokerAsync(string brokerAddress, int port)
    {
        try
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(brokerAddress, port) // 브로커 주소 및 포트
                .WithCredentials("admin", "vapor")
                .WithCleanSession()
                .Build();

            await _mqttClient.ConnectAsync(options);
            Console.WriteLine("MQTT 브로커에 성공적으로 연결되었습니다.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MQTT 브로커 연결 실패: {ex.Message}");
            throw;
        }
    }

    public async Task DisconnectAsync()
    {
        if (_mqttClient.IsConnected)
        {
            try
            {
                await _mqttClient.DisconnectAsync();
                Console.WriteLine("MQTT 연결이 해제되었습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MQTT 연결 해제 중 오류 발생: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("MQTT 클라이언트가 이미 연결 해제 상태입니다.");
        }
    }



    public async Task PublishCurrentDataAsync()
    {
        if (!_mqttClient.IsConnected)
        {
            throw new InvalidOperationException("MQTT 클라이언트가 연결되지 않았습니다.");
        }

        // JSON 직렬화
        string jsonData = JsonSerializer.Serialize(new
        {
            CurrentData.X20,
            CurrentData.X21,
            CurrentData.Y40,
            CurrentData.D1,
            CurrentData.D2,
            CurrentData.Y41,
            CurrentData.Y42,
            CurrentData.Y43
        });

        // MQTT 메시지 생성
        var message = new MqttApplicationMessageBuilder()
            .WithTopic("Process.PLC")
            .WithPayload(jsonData)
            .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
            .WithRetainFlag(false)
            .Build();

        // MQTT 메시지 전송
        await _mqttClient.PublishAsync(message);
        Debug.WriteLine(jsonData);
        Console.WriteLine("CurrentData가 MQTT로 전송되었습니다.");
    }
}