using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

	public Dialogue dialogue;

	public void TriggerDialogue()
	{
		Debug.Log("hello");
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

}