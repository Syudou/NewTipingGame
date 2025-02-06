using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProjectileController : MonoBehaviour
{
    public static event Action<WordController, ProjectileController> OnProjectileHitWord; // �C�x���g��`

    private WordController targetWord; // �^�[�Q�b�g�̕���
    public float speed = 10f; // �e�̈ړ����x

    
    public void Initialize(WordController target)
    {
        targetWord = target; // �^�[�Q�b�g��ݒ�
    }

    void Update()
    {
        if (targetWord == null)
        {
            // �^�[�Q�b�g�����ɔj�󂳂�Ă�����e������
            Destroy(gameObject);
            return;
        }

        // �^�[�Q�b�g�Ɍ������Ĉړ�
        Vector3 targetPosition = targetWord.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // �^�[�Q�b�g�ɓ��B������
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {

            // �C�x���g���s
            OnProjectileHitWord?.Invoke(targetWord, this);

            // �ՓˑΏۂƒe��j��
            Destroy(targetWord.gameObject);
            Destroy(gameObject);
        }



    }

    //public void ShootBullet(char inputChar)
    //{
    //    WordManager wordManager = FindObjectOfType<WordManager>();
    //    if (wordManager == null) return;

    //    // �ł��߂��^�[�Q�b�g���擾
    //    GameObject targetWordObject = wordManager.GetClosestWord(inputChar.ToString());
    //    if (targetWordObject == null) return;

    //    WordController targetWord = targetWordObject.GetComponent<WordController>();
    //    if (targetWord == null) return;

    //    // �^�[�Q�b�g���Z�b�g���Ēe�𔭎�
    //    Initialize(targetWord);
    //}




}
