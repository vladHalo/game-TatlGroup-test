using System.Collections.Generic;
using Core.Scripts.Views.Models;
using Sirenix.Utilities;
using UnityEngine;

namespace Core.Scripts.Views
{
    public class ActionButtonManager : MonoBehaviour
    {
        [SerializeField] private List<ButtonModel> _buttonsList;

        //private readonly string[] Prefixs = { "-Image", "-Color", "-Text" };

        private void Start()
        {
            _buttonsList.ForEach((item, index) =>
            {
                if (item.button != null)
                {
                    item.button.onClick.AddListener(() => { ChangeButton(index); });
                }
            });
            //StartChangeButton();
        }

        public void ChangeButton(int index)
        {
            if (_buttonsList[index].image != null && _buttonsList[index].sprites.Count >= 2)
            {
                int number = _buttonsList[index].image.sprite == _buttonsList[index].sprites[0] ? 1 : 0;
                _buttonsList[index].image.sprite = _buttonsList[index].sprites[number];
                // if (!string.IsNullOrEmpty(_buttonsList[index].nameSave))
                //     ES3.Save(_buttonsList[index].nameSave + Prefixs[0], number);
            }

            if (_buttonsList[index].image != null && _buttonsList[index].colors.Count >= 2)
            {
                int number = _buttonsList[index].image.color == _buttonsList[index].colors[0] ? 1 : 0;
                _buttonsList[index].image.color = _buttonsList[index].colors[number];
                // if (!string.IsNullOrEmpty(_buttonsList[index].nameSave))
                //     ES3.Save(_buttonsList[index].nameSave + Prefixs[1], number);
            }

            if (_buttonsList[index].text != null && _buttonsList[index].texts.Count >= 2)
            {
                int number = _buttonsList[index].text.text == _buttonsList[index].texts[0] ? 1 : 0;
                _buttonsList[index].text.text = _buttonsList[index].texts[number];
                // if (!string.IsNullOrEmpty(_buttonsList[index].nameSave))
                //     ES3.Save(_buttonsList[index].nameSave + Prefixs[2], number);
            }
        }

        // private void StartChangeButton()
        // {
        //     foreach (var t in _buttonsList)
        //     {
        //         if (string.IsNullOrEmpty(t.nameSave)) continue;
        //
        //         if (ES3.KeyExists(t.nameSave + Prefixs[0]) && t.image != null)
        //         {
        //             int index = ES3.Load<int>(t.nameSave + Prefixs[0]);
        //             t.image.sprite = t.sprites[index];
        //         }
        //
        //         if (ES3.KeyExists(t.nameSave + Prefixs[1]) && t.image != null)
        //         {
        //             int index = ES3.Load<int>(t.nameSave + Prefixs[1]);
        //             t.image.color = t.colors[index];
        //         }
        //
        //         if (ES3.KeyExists(t.nameSave + Prefixs[2]) && t.text != null)
        //         {
        //             int index = ES3.Load<int>(t.nameSave + Prefixs[2]);
        //             t.text.text = t.texts[index];
        //         }
        //     }
        // }
    }
}