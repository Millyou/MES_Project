using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp2.ViewInterface;

namespace WinFormsApp2.Presenter
{
    public class MainPresenter
    {
        private readonly IMainView _view;
        private readonly System.Windows.Forms.Timer _timer;

        public MainPresenter(IMainView view)
        {
            _view = view;
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 1000; // 1초마다 실행
            _timer.Tick += Timer_Tick; // Tick 이벤트 등록
            _timer.Start(); // 타이머 시작
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _view.UpdateLocalDateTime(currentTime); // View에 시간 업데이트 전달
        }

        
    }
}