using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	public bool isDialogueRunning;
	//public Text nameText;
	public TextMeshProUGUI dialogueText;

	public Animator animator;

	[HideInInspector]
	public Queue<string> sentences;

	[SerializeField] NPC npc;

	PlayerMovement playerMove;

	CharacterSwitch characterSwitch;

	void Start()
	{
		characterSwitch = GameObject.FindWithTag("Player").GetComponent<CharacterSwitch>();
		playerMove = FindObjectOfType<PlayerMovement>();
		sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue)
	{
		characterSwitch.isTalking = true;
		characterSwitch.SetIsPaused();
		isDialogueRunning = true;


		animator.SetBool("IsOpen", true);

		//nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		/*if(!npc.hasTalked && sentences.Count == indexToTakeOff)
        {
			//add new avaiable character if it's the case
        }*/

		if (sentences.Count == 0)
		{
			npc.hasTalked = true;
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	public void EndDialogue()
	{
		isDialogueRunning = false;
		animator.SetBool("IsOpen", false);

		characterSwitch.UNSetIsPaused();
		characterSwitch.isTalking = false;
	}

}