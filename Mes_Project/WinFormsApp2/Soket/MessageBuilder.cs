using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;
using MSD.Crux.Core.Models;

namespace MSD.Client.TCP;

/// <summary>
/// 소켓통신으로 전송할 데이터(메시지)를 만든다
/// </summary>
public static class MessageBuilder
{

    /// <summary>
    /// Injection 생산 누적생산량 소켓통신 프로토콜로 전송할 바이너리 데이터를 만든다
    /// </summary>
    /// <param name="frameType">프레임 타입 번호</param>
    /// <param name="injection">비전검사 누적 검사량 객체</param>
    /// <returns>소켓통신 바이너리 frame</returns>
    public static byte[] CreateInjectionTypeFrame(byte frameType, InjectionCum injectionCum)
    {
        int payloadSize = 50;
        int totalSize = 6 + payloadSize;
        byte[] message = new byte[totalSize];

        message[0] = frameType;
        BitConverter.GetBytes((ushort)payloadSize).CopyTo(message, 1);
        message[3] = 1;
        message[4] = 0;
        message[5] = 0;

        Encoding.ASCII.GetBytes(injectionCum.LineId.PadRight(4, '\0')).CopyTo(message, 6);
        BitConverter.GetBytes(new DateTimeOffset(injectionCum.Time).ToUnixTimeMilliseconds()).CopyTo(message, 10);
        Encoding.ASCII.GetBytes(injectionCum.LotId?.PadRight(20, '\0') ?? new string('\0', 20)).CopyTo(message, 18);
        Encoding.ASCII.GetBytes(injectionCum.Shift?.PadRight(4, ' ') ?? "    ").CopyTo(message, 38);
        BitConverter.GetBytes((injectionCum.EmployeeNumber ?? 0)).CopyTo(message, 42);
        BitConverter.GetBytes(injectionCum.Total).CopyTo(message, 46);

        return message;
    }

}
