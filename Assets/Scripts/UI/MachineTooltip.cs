using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace MonsterFactory
{
    public class MachineTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string machineName;
        MachineInfo machineInfo = new MachineInfo();

        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI costText;
        
        void Start()
        {
            machineInfo = BuildManager.Instance.FetchMachineInfo(machineName);
            nameText.text = machineInfo.MT_name;
            costText.text = "$" + machineInfo.MT_cost;

        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            //print("ye");
            MainUIManager.Instance.UIM_Description.text = machineInfo.MT_description;
            MainUIManager.Instance.MachineTooltipBox.SetActive(true);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            //print("Yo");
            MainUIManager.Instance.MachineTooltipBox.SetActive(false);
        }

        
    }
}
