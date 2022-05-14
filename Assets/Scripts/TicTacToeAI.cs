using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TicTacToeState{none, cross, circle}

[System.Serializable]
public class WinnerEvent : UnityEvent<int>
{
}

public class TicTacToeAI : MonoBehaviour
{

	int _aiLevel;

	TicTacToeState[,] boardState;

	[SerializeField]
	private bool _isPlayerTurn;

	[SerializeField]
	private int _gridSize = 3;
	
	[SerializeField]
	private TicTacToeState playerState = TicTacToeState.circle;
	TicTacToeState aiState = TicTacToeState.cross;

	[SerializeField]
	private GameObject _xPrefab;

	[SerializeField]
	private GameObject _oPrefab;

	public UnityEvent onGameStarted;

	//Call This event with the player number to denote the winner
	public WinnerEvent onPlayerWin;

	private int[,] _board;
	private int piecesOnTheBoard = 0;

	ClickTrigger[,] _triggers;
	
	private void Awake()
	{
		if(onPlayerWin == null){
			onPlayerWin = new WinnerEvent();
		}
	}

	public void StartAI(int AILevel){
		_aiLevel = AILevel;
		StartGame();
	}

	public void RegisterTransform(int myCoordX, int myCoordY, ClickTrigger clickTrigger)
	{
		_triggers[myCoordX, myCoordY] = clickTrigger;
	}

	private void StartGame()
	{
		_triggers = new ClickTrigger[_gridSize, _gridSize];
		_board = new int[_gridSize, _gridSize];

		for (int x = 0; x < 3; ++x)
		{
			for (int y = 0; y < 3; ++y)
			{
				_board[x, y] = (int) TicTacToeState.none;
			}
		}

		onGameStarted.Invoke();

		if (_aiLevel == 1)
		{
			playerState = TicTacToeState.cross;
			aiState = TicTacToeState.circle;
			AiSelects();
		}
	}

	public void PlayerSelects(int coordX, int coordY)
	{
		SetVisual(coordX, coordY, playerState);
		_board[coordX, coordY] = (int) playerState;
		CheckBoard();
		AiSelects();
	}

	public void AiSelects()
	{
		int coordX, coordY;
		(coordX, coordY) = BoardRules.MakeMove(_board, _gridSize, (int) aiState);

		SetVisual(coordX, coordY, aiState);
		_board[coordX, coordY] = (int) aiState;
		CheckBoard();
	}

	private void CheckBoard()
	{
		int boardState = BoardRules.CheckBoard(_board, _gridSize, (int) playerState, piecesOnTheBoard);

		if (boardState < 2)
		{
			onPlayerWin.Invoke(boardState);
		}
	}

	private void SetVisual(int coordX, int coordY, TicTacToeState targetState)
	{
		if (_triggers[coordX, coordY].checkInput())
		{
			_triggers[coordX, coordY].disableInput();
			++piecesOnTheBoard;

			Instantiate(
				targetState == TicTacToeState.circle ? _oPrefab : _xPrefab,
				_triggers[coordX, coordY].transform.position,
				Quaternion.identity
			);
		}
	}
}
