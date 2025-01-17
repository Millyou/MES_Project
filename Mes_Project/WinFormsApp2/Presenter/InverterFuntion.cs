using System;
using System.IO.Ports;
using WinFormsApp2.View;

namespace WinFormsApp2.Presenter
{
    public class InverterFunction
    {
        

        public  InverterFunction()
        {
            
        }
 
        
        public void SetFrequency()
        {
            try
            {
                ushort frequency = 5000;
                ModbusFrame frequencyFrame = new ModbusFrame(0x02, 0x06, 0x0004, frequency);
                SendFrame(frequencyFrame);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"오류 발생 (SetFrequency): {ex.Message}");
            }
        }
        public void Deceleration()
        {
            try
            {
                // 감속 명령 예제
                ModbusFrame decelerationFrame = new ModbusFrame(0x02, 0x06, 0x0007, 0x0001); // 감속 레지스터와 값
                SendFrame(decelerationFrame);
                Console.WriteLine("감속 명령 전송 완료.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"오류 발생 (Deceleration): {ex.Message}");
            }
        }

        public void Start()
        {
            try
            {
                ModbusFrame startFrame = new ModbusFrame(0x02, 0x06, 0x0005, 0x0502);
                SendFrame(startFrame);
                Console.WriteLine("운전 시작 명령 전송 완료.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"오류 발생 (Start): {ex.Message}");
            }
        }

        public void Stop()
        {
            try
            {
                ModbusFrame stopFrame = new ModbusFrame(0x02, 0x06, 0x0005, 0x0001);
                SendFrame(stopFrame);
                Console.WriteLine("운전 정지 명령 전송 완료.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"오류 발생 (Stop): {ex.Message}");
            }
        }

        private void SendFrame(ModbusFrame frame)
        {
            if (PlcFunction.serialPort == null || !PlcFunction.serialPort.IsOpen)
            {
                throw new InvalidOperationException("포트가 설정되지 않았거나 열려 있지 않습니다.");
            }

            byte[] frameBytes = frame.ToByteArray();
            PlcFunction.serialPort.Write(frameBytes, 0, frameBytes.Length);
            Console.WriteLine("Frame sent:");
            Console.WriteLine(BitConverter.ToString(frameBytes));
            Thread.Sleep(100);
        }
    }

    public class ModbusFrame
    {
        public byte SlaveID { get; }
        public byte FunctionCode { get; }
        public ushort RegisterAddress { get; }
        public ushort Value { get; }

        public ModbusFrame(byte slaveID, byte functionCode, ushort registerAddress, ushort value)
        {
            SlaveID = slaveID;
            FunctionCode = functionCode;
            RegisterAddress = registerAddress;
            Value = value;
        }

        public byte[] ToByteArray()
        {
            byte[] frame = new byte[6];
            frame[0] = SlaveID;
            frame[1] = FunctionCode;
            frame[2] = (byte)(RegisterAddress >> 8);
            frame[3] = (byte)(RegisterAddress & 0xFF);
            frame[4] = (byte)(Value >> 8);
            frame[5] = (byte)(Value & 0xFF);

            byte[] crc = CRC16.Calculate(frame);

            byte[] frameWithCRC = new byte[frame.Length + 2];
            frame.CopyTo(frameWithCRC, 0);
            frameWithCRC[frame.Length] = crc[0];
            frameWithCRC[frame.Length + 1] = crc[1];

            return frameWithCRC;
        }
    }

    public static class CRC16
    {
        public static byte[] Calculate(byte[] data)
        {
            ushort crc = 0xFFFF;

            foreach (byte b in data)
            {
                crc ^= b;
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }

            return new byte[] { (byte)(crc & 0xFF), (byte)((crc >> 8) & 0xFF) };
        }

       
    }


}
