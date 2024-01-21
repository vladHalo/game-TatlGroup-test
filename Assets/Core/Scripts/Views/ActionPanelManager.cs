using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.Views
{
    public class ActionPanelManager : MonoBehaviour
    {
        [SerializeField] private bool _onStartListener;

        [SerializeField] private ActionPanelType _actionPanelType;
        [SerializeField] private List<PanelViewModel> _panelViewModels;
        private readonly string Prefix = "-Panel";

        private void Start()
        {
            if (_panelViewModels.Count == 0) return;

            if (_onStartListener)
            {
                AddListener(GetMethod().Invoke);
            }

            StartOpenPanel();
        }

        public void AddListener(Action<int> method)
        {
            _panelViewModels.ForEach((item, index) =>
            {
                if (item.button != null)
                {
                    item.button.onClick.AddListener(() => { method(index); });
                }
            });
        }

        public Action<int> GetMethod()
        {
            Action<int> method = null;
            switch (_actionPanelType)
            {
                case ActionPanelType.OnePanel:
                    method = OpenPanels;
                    break;
                case ActionPanelType.ManyPanel:
                    method = OpenPanelsFromMany;
                    break;
            }

            return method;
        }

        public void OpenPanels(int index)
        {
            _panelViewModels[index].panels.ForEach(x =>
            {
                x.SetActive(!x.activeSelf);
                SavePanel(_panelViewModels[index].nameSave, x);
            });
        }

        public void OpenPanelsFromMany(int index)
        {
            _panelViewModels.ForEach(x =>
            {
                x.panels.ForEach(y =>
                {
                    y.SetActive(false);
                    SavePanel(x.nameSave, y);
                });
            });
            _panelViewModels[index].panels.ForEach(x =>
            {
                x.SetActive(true);
                SavePanel(_panelViewModels[index].nameSave, x);
            });
        }

        public void OpenPanelsFromMany<T>(T panelType) where T : Enum
        {
            int index = (int)(object)panelType;
            OpenPanelsFromMany(index);
        }

        private void StartOpenPanel()
        {
            _panelViewModels.ForEach(item => item.panels.ForEach(panel =>
            {
                if (!string.IsNullOrEmpty(item.nameSave))
                {
                    // if (ES3.KeyExists(item.nameSave + panel.name + Prefix))
                    // {
                    //     bool isActive = ES3.Load<bool>(item.nameSave + panel.name + Prefix);
                    //     panel.SetActive(isActive);
                    // }
                }
            }));
        }

        private void SavePanel(string nameSave, GameObject panel)
        {
            // if (!string.IsNullOrEmpty(nameSave))
            //     ES3.Save(nameSave + panel.name + Prefix, panel.activeSelf);
        }

        private enum ActionPanelType
        {
            OnePanel,
            ManyPanel
        }

        [Serializable]
        private class PanelViewModel
        {
            public string nameSave;
            public Button button;
            public List<GameObject> panels;
        }
    }
}