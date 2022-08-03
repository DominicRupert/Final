<template>
    <div class="component">


    </div>
</template>


<script>
import { computed, ref } from 'vue'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService.js'
import { useRoute } from 'vue-router'
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { accountService } from '../services/AccountService.js'
import { logger } from '../utils/Logger.js'
import { Modal } from 'bootstrap'
import  Pop  from '../utils/Pop.js'



export default {
    props: {
        vault: {
            type: Object,
            required: true
        }
    },
    setup(){
        const vault = ref({})
        const route = useRoute();
        return {
            account: computed(() => AppState.account),
            vault: computed(() => AppState.vaults),
            keep: computed(() => AppState.activeKeep),


            
            async createVaults(){
                try {
                    await vaultsService.createVaults(vault.value);
                }
                catch (error) {
                    logger.error(error);
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>

</style>