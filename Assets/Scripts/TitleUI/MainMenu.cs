using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class MainMenu : MonoBehaviour
{
	private BlurOptimized blurOptimized;

	[SerializeField] private GameObject vCamera;

	[SerializeField] private GameObject titleCanvas;
	[SerializeField] private GameObject menuCanvas;
	[SerializeField] private GameObject optionsCanvas;
	[SerializeField] private GameObject quitCanvas;

	[SerializeField] private RectTransform menuPanel;

	[SerializeField] private RectTransform menuCursor;
	[SerializeField] private RectTransform newGame;
	[SerializeField] private RectTransform loadGame;
	[SerializeField] private RectTransform options;
	[SerializeField] private RectTransform quitGame;

	[SerializeField] private RectTransform tutorialCursor;
	[SerializeField] private RectTransform tutorialYes;
	[SerializeField] private RectTransform tutorialNo;

	[SerializeField] private RectTransform quitCursor;
	[SerializeField] private RectTransform quitYes;
	[SerializeField] private RectTransform quitNo;
	[SerializeField] private Vector3 offset = new Vector3(0, 0, 0);

	private bool upTrigger = false;
	private bool downTrigger = false;

	Animator animator;
	// Use this for initialization
	void Start()
	{
		animator = GetComponent<Animator>();
		blurOptimized = Camera.main.GetComponent<BlurOptimized>();
	}

	// Update is called once per frame
	void Update()
	{
		inputBool();
		objectSetActive();
		menuCanvasControll();
		menuCursorControll();
		tutorialCursorControll();
		quitCursorControll();
		loadScene();
		quitApplication();
	}

	void inputBool()
	{
		if (Input.GetButtonDown("A")
			|| Input.GetButtonDown("B")
			|| Input.GetButtonDown("X")
			|| Input.GetButtonDown("Y")
			|| Input.GetButtonDown("LB")
			|| Input.GetButtonDown("RB")
			|| Input.GetButtonDown("View")
			|| Input.GetButtonDown("Menu")) {
			animator.SetBool("AnyButton", true);
		}
		else {
			animator.SetBool("AnyButton", false);
		}

		if (Input.GetButtonDown("A")) {
			animator.SetBool("Submit", true);
		}
		else {
			animator.SetBool("Submit", false);
		}

		if (Input.GetButtonDown("B")) {
			animator.SetBool("Cancel", true);
		}
		else {
			animator.SetBool("Cancel", false);
		}

		if (Input.GetAxisRaw("DPAD_v") == 1) {
			if (upTrigger == false) {
				animator.SetBool("Up", true);
				upTrigger = true;
			}
			else {
				animator.SetBool("Up", false);
				return;
			}
		}
		if (Input.GetAxisRaw("DPAD_v") == 0) {
			upTrigger = false;
		}

		if (Input.GetAxisRaw("DPAD_v") == -1) {
			if (downTrigger == false) {
				animator.SetBool("Down", true);
				downTrigger = true;
			}
			else {
				animator.SetBool("Down", false);
				return;
			}
		}
		if (Input.GetAxisRaw("DPAD_v") == 0) {
			downTrigger = false;
		}
	}
	void objectSetActive()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Title")) {
			titleCanvas.SetActive(true);
			vCamera.SetActive(true);
		}
		else {
			titleCanvas.SetActive(false);
			vCamera.SetActive(false);
		}

		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Menu_NewGame")
			|| animator.GetCurrentAnimatorStateInfo(0).IsName("Menu_LoadGame")
			|| animator.GetCurrentAnimatorStateInfo(0).IsName("Menu_Options")
			|| animator.GetCurrentAnimatorStateInfo(0).IsName("Menu_QuitGame")
			|| animator.GetCurrentAnimatorStateInfo(0).IsName("TutorialCheck_Yes")
			|| animator.GetCurrentAnimatorStateInfo(0).IsName("TutorialCheck_No")) {
			menuCanvas.SetActive(true);
		}
		else {
			menuCanvas.SetActive(false);
		}

		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Options")) {
			optionsCanvas.SetActive(true);
			blurOptimized.enabled = true;
		}
		else {
			optionsCanvas.SetActive(false);
			blurOptimized.enabled = false;
		}

		if (animator.GetCurrentAnimatorStateInfo(0).IsName("QuitCheck_No")
			|| animator.GetCurrentAnimatorStateInfo(0).IsName("QuitCheck_Yes")) {
			quitCanvas.SetActive(true);
		}
		else {
			quitCanvas.SetActive(false);
		}
	}
	void menuCanvasControll()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("TutorialCheck_Yes")
			|| animator.GetCurrentAnimatorStateInfo(0).IsName("TutorialCheck_No")) {
			menuPanel.transform.localPosition = new Vector3(-900, 0, 0);
		}
		else {
			menuPanel.transform.localPosition = new Vector3(-300, 0, 0);
		}
	}
	void menuCursorControll()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Menu_QuitGame")) {
			menuCursor.transform.position = quitGame.transform.position + offset;
		}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Menu_Options")) {
			menuCursor.transform.position = options.transform.position + offset;
		}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Menu_LoadGame")) {
			menuCursor.transform.position = loadGame.transform.position + offset;
		}
		else {
			menuCursor.transform.position = newGame.transform.position + offset;
		}
	}
	void tutorialCursorControll()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("TutorialCheck_Yes")) {
			tutorialCursor.transform.position = tutorialYes.transform.position + offset;
		}
		else {
			tutorialCursor.transform.position = tutorialNo.transform.position + offset;
		}
	}
	void quitCursorControll()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("QuitCheck_Yes")) {
			quitCursor.transform.position = quitYes.transform.position + offset;
		}
		else {
			quitCursor.transform.position = quitNo.transform.position + offset;
		}
	}
	void loadScene()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("LoadTutorial")) {
			SceneManager.LoadScene("Tutorial");
		}
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("LoadStage1")) {
			SceneManager.LoadScene("TestRange");
		}
	}
	void quitApplication()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("QuitGame")) {
			Application.Quit();
		}
	}
}