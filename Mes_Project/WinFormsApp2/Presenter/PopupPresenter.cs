using System;
using System.Collections.Generic;
using WinFormsApp2.ViewInterface;
using WinFormsApp2.Model;

namespace WinFormsApp2.Presenter
{
    public class PopupPresenter
    {
        private readonly IPopupView _view;
        private readonly PlcModel _model;

        public PopupPresenter(IPopupView view, PlcModel model)
        {
            _view = view;
            _model = model;
        }

        public void LoadStations()
        {
            try
            {
                var stations = _model.GetAvailableStations();

                if (stations == null || stations.Count == 0)
                {
                    _view.ShowErrorMessage("사용 가능한 Station이 없습니다.");
                }
                else
                {
                    // Station 번호를 문자열 리스트로 변환
                    var stationStrings = stations.ConvertAll(station => $"Station {station}");
                    _view.PopulatePortCombo(stationStrings);
                }
            }
            catch (Exception ex)
            {
                _view.ShowErrorMessage($"Station 로드 중 오류가 발생했습니다.\n{ex.Message}");
            }
        }
    }
}