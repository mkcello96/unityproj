using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

	public Text nameText;
	public Text dialogueText;

	public GameObject dialoguePanel;

	public bool dialogueModeEnabled = false;

	//public Animator animator;

	private Queue<string> diagSegments;

	// Use this for initialization
	void Start()
	{
		diagSegments = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue)
	{
		//animator.SetBool("IsOpen", true);
		dialogueModeEnabled = true;
		dialoguePanel.SetActive(true);

		nameText.text = dialogue.charName;

		diagSegments.Clear();

		foreach (string sentence in dialogue.diagSegments)
		{
			diagSegments.Enqueue(sentence);
		}

		DisplayNextSegment();
	}

	public void DisplayNextSegment()
	{
		if (diagSegments.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = diagSegments.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSegment(sentence));
	}

	IEnumerator TypeSegment(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.01f);
		}
	}

	void EndDialogue()
	{
		//animator.SetBool("IsOpen", false);
		dialogueModeEnabled = false;
		dialoguePanel.SetActive(false);
	}

}