<template>
   <Modal id="keep-modal">
   <!-- <template #modal-title>{{keep.name}}</template> -->
    <template #modal-body>
        <div class="container">
            <div class="col-md-6">
                <h3>{{keep.name}}</h3>
                <!-- <p>{{keep.description}}</p> -->
                <img :src="keep.img" class="img-fluid" alt="">
                <!-- <p @click="goToProfile">{{keep.name}}</p> -->
                <img :src="keep.img" class="img-fluid py-2" alt="">
                <button  class="btn btn-dark"> <h3>Add To Vault</h3></button>
            </div>
            
        </div>
    </template>
   
   </Modal>
</template>


<script>
import { AppState } from '../AppState.js'
import { computed, reactive } from 'vue'
import { useRouter } from 'vue-router'
import { logger } from '../utils/Logger.js'
import { keepsService } from '../services/KeepsService.js'
import Pop from '../utils/Pop.js'

export default {
 
    setup(){
        const editable = ref({keepId: AppState.activeKeep.id})
        const router = useRouter()
        return {
            editable,
            account: computed(()=>AppState.account),
            activeKeep: computed(()=>AppState.activeKeep),


            keep: computed(()=>AppState.keeps),
            vaults: computed(()=>AppState.vaults),
            user: computed(()=>AppState.user),

            async newVaultKeep(){
                try{
                    editable.vaultId = vault.id
                    const vaultKeep = await vaultKeepsService.createVaultKeep(editable)
                    Pop.toast('Keep Added To Vault')
                }catch(e){
                    logger.error(e)
                    Pop.toast(e.message)
                }
            } ,
            goToProfile(){
                router.push({name: 'Profile', params: {id: props.keep.creatorId}})
            },
        }
    }
}
</script>


<style lang="scss" scoped>

</style>