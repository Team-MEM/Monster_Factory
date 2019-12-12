namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class MachineTooltip : MainUIManager, IPointerEnterHandler, IPointerExitHandler
    {
        public string machineName;
        MachineInfo machineInfo = new MachineInfo();
        
        void Start()
        {
            machineInfo = BuildManager.instance.FetchMachineInfo(machineName);
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            UIM_Name.text = machineInfo.MT_name;
            UIM_Cost.text = "" + machineInfo.MT_cost;
            UIM_Description.text = machineInfo.MT_description;

            MachineTooltipBox.SetActive(true);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            MachineTooltipBox.SetActive(false);
        }

        
    }
}
