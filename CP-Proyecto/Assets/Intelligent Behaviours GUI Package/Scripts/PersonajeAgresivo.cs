using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonajeAgresivo : MonoBehaviour {

    #region variables

    private StateMachineEngine PersonajeAgresivo_FSM;
    

    private PushPerception inactivohayEnemigoPerception;
    private PushPerception inactivohayMonedaPerception;
    private PushPerception inactivohayArmaduraPerception;
    private PushPerception hayArmaduraPerception;
    private PushPerception hayEnemigoPerception;
    private PushPerception inactivohayArmaPerception;
    private PushPerception hayArmaPerception;
    private PushPerception hayMonedaPerception;
    private PushPerception hayMonedahayEnemigoPerception;
    private PushPerception hayArmadurahayEnemigoPerception;
    private PushPerception hayArmahayEnemigoPerception;
    private State Inactivo;
    private State Combatir;
    private State cogerMoneda;
    private State cogerAmadura;
    private State cogerArma;
    
    //Place your variables here

    #endregion variables

    // Start is called before the first frame update
    private void Start()
    {
        PersonajeAgresivo_FSM = new StateMachineEngine(false);
        

        CreateStateMachine();
    }
    
    
    private void CreateStateMachine()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        inactivohayEnemigoPerception = PersonajeAgresivo_FSM.CreatePerception<PushPerception>();
        inactivohayMonedaPerception = PersonajeAgresivo_FSM.CreatePerception<PushPerception>();
        inactivohayArmaduraPerception = PersonajeAgresivo_FSM.CreatePerception<PushPerception>();
        hayArmaduraPerception = PersonajeAgresivo_FSM.CreatePerception<PushPerception>();
        hayEnemigoPerception = PersonajeAgresivo_FSM.CreatePerception<PushPerception>();
        inactivohayArmaPerception = PersonajeAgresivo_FSM.CreatePerception<PushPerception>();
        hayArmaPerception = PersonajeAgresivo_FSM.CreatePerception<PushPerception>();
        hayMonedaPerception = PersonajeAgresivo_FSM.CreatePerception<PushPerception>();
        hayMonedahayEnemigoPerception = PersonajeAgresivo_FSM.CreatePerception<PushPerception>();
        hayArmadurahayEnemigoPerception = PersonajeAgresivo_FSM.CreatePerception<PushPerception>();
        hayArmahayEnemigoPerception = PersonajeAgresivo_FSM.CreatePerception<PushPerception>();
        
        // States
        Inactivo = PersonajeAgresivo_FSM.CreateEntryState("Inactivo", InactivoAction);
        Combatir = PersonajeAgresivo_FSM.CreateState("Combatir", CombatirAction);
        cogerMoneda = PersonajeAgresivo_FSM.CreateState("cogerMoneda", cogerMonedaAction);
        cogerAmadura = PersonajeAgresivo_FSM.CreateState("cogerAmadura", cogerAmaduraAction);
        cogerArma = PersonajeAgresivo_FSM.CreateState("cogerArma", cogerArmaAction);
    
        // Transitions
        PersonajeAgresivo_FSM.CreateTransition("inactivo-hayEnemigo", Inactivo, inactivohayEnemigoPerception, Combatir);
        PersonajeAgresivo_FSM.CreateTransition("inactivo-hayMoneda", Inactivo, inactivohayMonedaPerception, cogerMoneda);
        PersonajeAgresivo_FSM.CreateTransition("inactivo-hayArmadura", Inactivo, inactivohayArmaduraPerception, cogerAmadura);
        PersonajeAgresivo_FSM.CreateTransition("!hayArmadura", cogerAmadura, hayArmaduraPerception, Inactivo);
        PersonajeAgresivo_FSM.CreateTransition("!hayEnemigo", Combatir, hayEnemigoPerception, Inactivo);
        PersonajeAgresivo_FSM.CreateTransition("inactivo-hayArma", Inactivo, inactivohayArmaPerception, cogerArma);
        PersonajeAgresivo_FSM.CreateTransition("!hayArma", cogerArma, hayArmaPerception, Inactivo);
        PersonajeAgresivo_FSM.CreateTransition("!hayMoneda", cogerMoneda, hayMonedaPerception, Inactivo);
        PersonajeAgresivo_FSM.CreateTransition("hayMoneda-hayEnemigo", cogerMoneda, hayMonedahayEnemigoPerception, Combatir);
        PersonajeAgresivo_FSM.CreateTransition("hayArmadura-hayEnemigo", cogerAmadura, hayArmadurahayEnemigoPerception, Combatir);
        PersonajeAgresivo_FSM.CreateTransition("hayArma-hayEnemigo", cogerArma, hayArmahayEnemigoPerception, Combatir);
        
        // ExitPerceptions
        // ExitTransitions
        
    }

    // Update is called once per frame
    private void Update()
    {
        PersonajeAgresivo_FSM.Update();
    }

    // Create your desired actions
    
    private void InactivoAction()
    {
        
    }
    
    private void CombatirAction()
    {
        
    }
    
    private void cogerMonedaAction()
    {
        
    }
    
    private void cogerAmaduraAction()
    {
        
    }
    
    private void cogerArmaAction()
    {
        
    }
    
}