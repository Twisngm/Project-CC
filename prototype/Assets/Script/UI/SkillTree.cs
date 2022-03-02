using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SkillTree : MonoBehaviour
{
    public GameObject SkillTextWindow;

    public string SkillText;
    public bool isActive;

    private Camera _uiCamera;
    private Collider _collider;

    private bool _isMouseOver;
    private bool _isMousePushed;

    private void Awake()
    {
        _uiCamera = FindObjectOfType<Camera>(); // 해당 오브젝트를 비추고 있는 카메라
        _collider = GetComponent<Collider>(); // 오브젝트의 Collider
    }
    // Start is called before the first frame update
    void Start()
    {
      //  SkillTextWindow = GameObject.Find("SkillTree_Compartment").transform.Find("SkillText").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = _uiCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

      //  var leftMouse = (int)MouseButton.LeftMouse; // Test를 위해 왼쪽 버튼을 입력으로 받는다.

        if (hit.collider != null && hit.collider == _collider && !_isMouseOver)
        {
            _isMouseOver = true;
            OnMouseEnter();
        }
        else if (hit.collider != _collider && _isMouseOver)
        {
            _isMouseOver = false;
            OnMouseExit();
        }
        /*
        if (_isMouseOver) OnMouseOver();
        if (_isMouseOver && Input.GetMouseButtonDown(leftMouse))
        {
            _isMousePushed = true;
            OnMouseDown();
        }
        if (_isMousePushed && Input.GetMouseButton(leftMouse)) OnMouseDrag();
        if (_isMousePushed && Input.GetMouseButtonUp(leftMouse))
        {
            _isMousePushed = false;
            OnMouseUp();
            if (_isMouseOver) OnMouseUpAsButton();
        }*/

        if (!isActive)
        {
            this.gameObject.GetComponent<Image>().color = Color.gray;
        }
        else
            this.gameObject.GetComponent<Image>().color = Color.white;

        
    }

    private void OnMouseEnter()
    {
        SkillTextWindow.SetActive(true);
        SkillTextWindow.GetComponentInChildren<Text>().text = SkillText;
    }

    private void OnMouseExit()
    {
        SkillTextWindow.SetActive(false);
    }

 

    private void OnMouseUpAsButton()
    {
        if (SkillTreeDB.Instance.Skill_Point > 0)
        {
            SkillTreeDB.Instance.Skill_Point--;
            switch (this.gameObject.name)
            {

                case "1":
                    PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[0] = PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[0] == true ? false : true;
                    break;

                case "2-1":
                    PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[1] = PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[1] == true ? false : true;
                    break;

                case "2-2":
                    PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[2] = PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[2] == true ? false : true;
                    break;

                case "3-1":
                    PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[3] = PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[3] == true ? false : true;
                    break;

                case "3-2":
                    PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[4] = PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[4] == true ? false : true;
                    break;

                case "4-1":
                    PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[5] = PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[5] == true ? false : true;
                    break;

                case "4-2":
                    PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[6] = PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[6] == true ? false : true;
                    break;

                case "5-1":
                    PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[7] = PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[7] == true ? false : true;
                    break;

                case "6-1":
                    PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[8] = PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[8] == true ? false : true;
                    break;

                case "6-2":
                    PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[9] = PartyManager.Instance.PartyList[InventoryManager.Instance.CurrentIndex].gameObject.GetComponent<ClassSkillTree>().SkillTree[9] == true ? false : true;
                    break;

            }
            isActive = isActive == true ? false : true;
        }
    }


}
