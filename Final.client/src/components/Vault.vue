<template>
<div>{{vault.name}}</div>
  


</template>


<script>
import { computed, onMounted } from 'vue'
import { logger } from '../utils/Logger.js'
import { AppState } from '../AppState.js'
import { vaultsService } from '../services/VaultsService.js'
import { useRouter } from 'vue-router'
export default {
    props: {
        vault: {
            type: Object,
            required: true
        }
    },
    setup(props){
        const router = useRouter()
        return {
            vaults: computed(() => AppState.vaults),
            account: computed(() => AppState.account),
            profile: computed(() => AppState.profile),
            pvaults: computed(() => AppState.profileVaults),
            
            // avaults: computed(() => AppState.),
       
        }
    }
}
</script>


<style lang="scss" scoped>

</style>