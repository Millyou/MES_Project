using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp2.Presenter;

namespace WinFormsApp2.View
{
    public partial class Order : Form
    {
        public static event Action? OderUpdated;
        public static string todayDate;
        public Order()
        {
            InitializeComponent();
            todayDate = DateTime.Now.ToString("yyyy-MM-dd"); // 오늘 날짜 데이터 확인 완료

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using var httpClient = new HttpClient { BaseAddress = new Uri("http://13.125.114.64:5282/api/") };
            Form1 form = new Form1();
            try
            {
                // API 요청 보내기
                HttpResponseMessage response = await httpClient.GetAsync($"Plan/day/{todayDate}");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // JSON 데이터를 Dictionary로 파싱
                    var responseData = JsonSerializer.Deserialize<Dictionary<string, int>>(responseBody);

                    if (responseData != null)
                    {
                        // 기존 DataGridView의 데이터를 초기화
                        dataGridView1.Rows.Clear();

                        // Dictionary 데이터를 DataGridView에 추가
                        foreach (var kvp in responseData)
                        {
                            // 제품코드(Key) -> 첫 번째 열, 목표수량(Value) -> 두 번째 열
                            dataGridView1.Rows.Add(kvp.Key, kvp.Value);
                        }
                        
                        form.StartBt.Enabled = true;
                        form.EndBt.Enabled = true;
                        MessageBox.Show("데이터를 성공적으로 가져왔습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("데이터를 파싱할 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"오류 코드: {response.StatusCode}", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"조회 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
           
            try
            {
                // 선택된 행 확인
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // 선택된 행 가져오기 (첫 번째 선택된 행)
                    var selectedRow = dataGridView1.SelectedRows[0];

                    // 각 열의 데이터를 가져오기
                    var partId = selectedRow.Cells[0].Value?.ToString(); // 첫 번째 열 (제품코드)
                    var plannQtyString = selectedRow.Cells[1].Value?.ToString(); // 두 번째 열 (목표수량)

                    // 제품코드 저장
                    Products.PartId = partId;

                    // 목표수량(int) 변환
                    if (int.TryParse(plannQtyString, out int plannQty))
                    {
                        Products.PlannQty = plannQty;

                        // 확인 메시지
                        MessageBox.Show($"저장 완료:\nPartId: {Products.PartId}\nPlannQty: {Products.PlannQty}", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        using var httpClient = new HttpClient { BaseAddress = new Uri("http://13.125.114.64:5282/api/") };

                        try
                        {
                            var partId_Date = new { partId = Products.PartId, date = todayDate };

                            // JSON 직렬화
                            string jsonContent = JsonSerializer.Serialize(partId_Date);
                            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                            // API 요청 보내기
                            HttpResponseMessage response = await httpClient.PostAsync("lot/issue-new", httpContent);

                            if (response.IsSuccessStatusCode)
                            {
                                string responseBody = await response.Content.ReadAsStringAsync();

                                // JSON 파싱
                                var responseData = JsonSerializer.Deserialize<JsonDocument>(responseBody);
                                if (responseData != null && responseData.RootElement.TryGetProperty("lotId", out var lotIdElement))
                                {
                                    Products.LotNum = lotIdElement.GetString();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"lot 생성중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        OderUpdated?.Invoke();
                        this.Close();
                    }

                    else
                    {
                        MessageBox.Show("목표수량 값을 정수로 변환할 수 없습니다.", "변환 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    // 선택된 행이 없는 경우
                    MessageBox.Show("행을 선택해주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
