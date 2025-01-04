using System;
using System.Collections.Generic;
using ActUtlTypeLib;

public class PlcModel
{
    
    private static readonly ActUtlType _plc = new ActUtlType();
    public static bool IsConnected { get; private set; }
    public static int StationNumber { get; set; }

    /// <summary>
    /// 사용 가능한 MX Component Station 번호 가져오기
    /// </summary>
    /// <returns>사용 가능한 Station 번호 목록</returns>
    public List<int> GetAvailableStations()
    {
        List<int> availableStations = new List<int>();

        for (int i = 1; i <= 10; i++) // MX Component는 최대 64개의 Station 지원
        {
            try
            {
                _plc.ActLogicalStationNumber = i;
                int result = _plc.Open();

                if (result == 0) // 연결 성공
                {
                    availableStations.Add(i);
                    _plc.Close(); // 연결 해제
                }
            }
            catch
            {
                // 연결 실패 시 무시하고 다음 Station 검사
            }
        }

        return availableStations;
    }

    /// <summary>
    /// 특정 Station에 연결
    /// </summary>
    /// <param name="stationNumber">연결할 Station 번호</param>
    public static void Connect(int stationNumber)
    {
        _plc.ActLogicalStationNumber = stationNumber;
        int result = _plc.Open();
        if (result != 0)
        {
            throw new InvalidOperationException($"PLC 연결 실패 (Station: {stationNumber})");
        }
        IsConnected = true;
    }

    public static void Disconnect()
    {
        if (IsConnected)
        {
            _plc.Close();
            IsConnected = false;
        }
    }
}