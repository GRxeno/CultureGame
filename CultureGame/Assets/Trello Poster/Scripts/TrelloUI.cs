using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Trello;
using TMPro;

public class TrelloUI : MonoBehaviour
{
	private static readonly string[] TrelloCardPositions = { "top", "bottom" };

	[SerializeField]
	private TrelloPoster trelloPoster;
	//[SerializeField]
	//private GameObject trelloCanvas;
	//[SerializeField]
	//private GameObject reportPanel;
	[SerializeField]
	private TMP_InputField cardName;
	[SerializeField]
	private TMP_InputField cardDesc;
	//[SerializeField]
	//private Dropdown cardPosition;
	[SerializeField]
	private TMP_Dropdown cardList;
	[SerializeField]
	private TMP_Dropdown cardLabel;
	[SerializeField]
	private Toggle includeScreenshot;

	private Texture2D screenshot;
	private bool noLabels = false;

	private void Start()
	{
		cardList.AddOptions(GetDropdownOptions(trelloPoster.TrelloCardListOptions));
		cardList.value = 0;
		TrelloCardOption[] cardLabels = trelloPoster.TrelloCardLabelOptions;
		if (cardLabels == null || cardLabels.Length == 0)
		{
			noLabels = true;
			cardLabel.gameObject.SetActive(false);
		}
		else
		{
			cardLabel.AddOptions(GetDropdownOptions(cardLabels));
			cardLabel.value = 1;
		}

	}

	public void StartPostCard()
	{
		if (!cardName.text.Equals("") || !cardDesc.text.Equals(""))
		{
			StartCoroutine(trelloPoster.PostCard(new TrelloCard(cardName.text, cardDesc.text, TrelloCardPositions[0], trelloPoster.TrelloCardListOptions[cardList.value].Id, noLabels ? null : trelloPoster.TrelloCardLabelOptions[cardLabel.value].Id, includeScreenshot.isOn ? screenshot.EncodeToPNG() : null)));
		}
	}

	private List<TMP_Dropdown.OptionData> GetDropdownOptions(TrelloCardOption[] cardOptions)
	{
		List<TMP_Dropdown.OptionData> dropdownOptions = new List<TMP_Dropdown.OptionData>();
		for (int i = 0; i < cardOptions.Length; i++)
		{
			dropdownOptions.Add(new TMP_Dropdown.OptionData(cardOptions[i].Name));
		}
		return dropdownOptions;
	}
	/*
	public void ToggleCanvas()
	{
		trelloCanvas.SetActive(!trelloCanvas.activeSelf);
	}

	public void ToggleCanvas(bool isEnabled)
	{
		trelloCanvas.SetActive(isEnabled);
	}

	public void TogglePanel()
	{
		reportPanel.SetActive(!reportPanel.activeSelf);
	}*/

	public void TakeScreenshot()
	{
		StartCoroutine(RecordFrame());
	}

	IEnumerator RecordFrame()
	{
		yield return new WaitForEndOfFrame();
		screenshot = ScreenCapture.CaptureScreenshotAsTexture();

	}

	public void ResetUI()
	{
		cardName.text = "";
		cardDesc.text = "";
		includeScreenshot.isOn = true;
		cardLabel.value = 1;
		cardList.value = 0;
	}
}
