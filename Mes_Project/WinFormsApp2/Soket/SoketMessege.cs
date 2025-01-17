using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using MSD.Client.TCP;
using MSD.Crux.Core.Models;
using WinFormsApp2;
using WinFormsApp2.Presenter;

/// <summary>
/// 소켓통신 커스텀 프로토콜 클라이언트 로직
/// </summary>
public class SocketClient
{
    /// <summary>
    /// InjectionCum 객체 생성
    /// </summary>
    public static InjectionCum CreateInjectionCum()
    {
        Form1 form = new Form1();
        return new InjectionCum
        {
            LineId = "ip0" + Popup.StationNumber.ToString(),
            Time = DateTime.UtcNow,
            LotId = Products.LotNum,
            Shift = ApiToken.Shift,
            EmployeeNumber = ApiToken.EmployeeNumber,
            Total = CurrentData.D2 ?? 0
        };
    }

    /// <summary>
    /// 소켓 서버에 연결
    /// </summary>
    public static async Task<NetworkStream> ConnectToServerAsync(string serverAddress, int port)
    {
        try
        {
            TcpClient client = new TcpClient();
            Console.WriteLine($"서버에 연결중 {serverAddress}:{port}...");
            await client.ConnectAsync(serverAddress, port);
            Console.WriteLine("서버에 연결됨!");

            return client.GetStream();
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"소켓 에러: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"서버 연결 에러: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// 메시지 전송
    /// </summary>
    public static async Task SendMessageAsync(NetworkStream stream, InjectionCum injectionCum, CancellationToken cancellationToken)
    {
        try
        {
            byte[] message = BuildMessage(injectionCum);

            // 메시지 전송
            await stream.WriteAsync(message, 0, message.Length, cancellationToken);

            // Total 값 디코딩 및 디버깅
            DecodeAndDisplayTotal(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"메시지 빌더 에러: {ex.Message}");
        }
    }

    /// <summary>
    /// 메시지 생성
    /// </summary>
    public static byte[] BuildMessage(InjectionCum injectionCum)
    {
        return MessageBuilder.CreateInjectionTypeFrame(1, injectionCum);
    }

    /// <summary>
    /// 메시지에서 Total 값을 디코딩하고 출력
    /// </summary>
    public static void DecodeAndDisplayTotal(byte[] message)
    {
        try
        {
            int total = BitConverter.ToInt32(message, 10); // 50번 오프셋에서 Total 값 추출
            byte[] totalBytes = BitConverter.GetBytes(total);

            // 바이트 배열 출력
            Console.WriteLine("Byte array for Total:");
            //MessageBox.Show(string.Join(" ", totalBytes.Select(b => b.ToString("X2"))));

            //MessageBox.Show($"Decoded Total: {total}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Total 디코딩 에러: {ex.Message}");
        }
    }
}

// Example usage from another class
public class ExampleUsage
{
    public static async Task ExecuteSocketCommunicationAsync()
    {
        const string serverAddress = "13.125.114.64";
        const int port = 51900;

        try
        {
            // Connect to the server
            var stream = await SocketClient.ConnectToServerAsync(serverAddress, port);

            // Create InjectionCum object
            var injectionCum = SocketClient.CreateInjectionCum();

            // Send a message
            await SocketClient.SendMessageAsync(stream, injectionCum, CancellationToken.None);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"에러 발생: {ex.Message}");
        }
    }
}
